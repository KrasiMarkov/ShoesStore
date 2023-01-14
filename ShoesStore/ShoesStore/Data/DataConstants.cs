using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Data
{
    public class DataConstants
    {

        public class User
        { 
            public const int FullNameMinLength = 5;
            public const int FullNameMaxLength = 40;
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;
        }
        public class Shoe 
        {
            public const int BrandMinLength = 2;
            public const int BrandMaxLength = 30;
            public const int ModelMinLength = 2;
            public const int ModelMaxLength = 50;
            public const int SizeMinValue = 20;
            public const int SizeMaxValue = 50;
            public const int ColorMinLength = 2;
            public const int ColorMaxLength = 20;
            public const int MatterMinLength = 2;
            public const int MatterMaxLength = 30;
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 2000;
            public const double PriceMinValue = 0;
            public const double PriceMaxValue = 10000;
        }

        public class Category 
        {
            public const int NameMaxLength = 25;
        }

        public class Seller 
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 25;
            public const int PhoneNumberMinLength = 6;
            public const int PhoneNumberMaxLength = 30;


        }

    }
}
