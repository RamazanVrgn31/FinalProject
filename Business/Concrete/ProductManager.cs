﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Castle.Components.DictionaryAdapter;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConserns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;

namespace Business.Concrete
{
    public class ProductManager:IProductService
    {
        private IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;


        }

        public IDataResult<List<Product>> GetAll()
        {
            //İş kodları
            //Yetkisi var mı?

            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new  SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>( _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>( _productDal.GetProductDetails());
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>( _productDal.Get(p => p.ProductId == productId));
        }


        [SecuredOperation("product.add,admin")]
        //[ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {

            IResult result= BusinessRules.Run(CheckIfProductOfCategoryCorrect(product.CategoryId),
                CheckProductNameExist(product.ProductName),CheckIfCategoryLimitExceded());


            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);

            //Başarılı ve bunun yanında mesaj döndermek istersek
            return new SuccessResult(Messages.ProductAdded);

            //Sadece başarılı ise.
            //return new SuccessResult();

            //ValidationTool.Validate(new ProductValidator(), product);
            return new ErrorResult();
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult();
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult();


        }

        //Bir categoride 10 üründen fazla ürün olamaz.
        private IResult CheckIfProductOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count();
            if (result >10)
            {
                return new ErrorResult(Messages.ProductOfCategoryError);
            }

            return new SuccessResult();
        }
        //Ürün iki defa eklenemez.
        private IResult CheckProductNameExist(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }

            return new SuccessResult();
        }

        //Kategoriler 15'i geçerse ürün eklenemez.
        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }

            return new SuccessResult();
        }



    }


}
