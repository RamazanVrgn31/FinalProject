using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded="Ürün ekleme işlemi başarılı.";
        public static string ProductNameInvalid = "Ürün ismi geçersiz.";
        public static string MaintenanceTime = "Sistem bakımda.";
        public static string ProductListed = "Ürünler Listelendi.";
        public static string ProductOfCategoryError = "Bir kategorideki ürün adedini geçtiniz.";
        public static string ProductNameAlreadyExist = "Bu isimde zaten bir ürün var.";
        public static string CategoryLimitExceded = "Categori limiti aşıldığı için yeni ürün eklenemiyor.";
    }
}
