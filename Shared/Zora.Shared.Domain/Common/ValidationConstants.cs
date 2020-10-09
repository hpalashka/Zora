namespace Zora.Shared.Domain.Common
{
    public class ValidationConstants
    {

        public const string Title = "Заглавие";
        public const string Description = "Описание";
        public const string CoverPhoto = "Заглавна страница";
        public const string CreatedDate = "Дата на създаване";
        public const string UpdatedDate = "Дата на корекция";
        public const string ImagesCount = "Брой снимки в албума";
        public const string Album = "Албум";
        public const string StartDate = "Начална дата";
        public const string EndDate = "Крайна дата";
        public const string Active = "Активен";
        public const string FullName = "Две имена";
        public const string Messsage = "Съобщение";
        public const string UploadImage = "Качване снимка";
        public const string Email = "Имейл адрес";
        public const string UserName = "Потребителско име";
        public const string Image = "Снимка";
        public const string Address = "Адрес";
        public const string Phone = "Телефон";
        public const string Password = "Парола";
        public const string UploadUserPhoto = "Качване на профилна снимка";
        public const string RememberMe = "Запомни ме?";
        public const string UserPhoto = "Профилна снимка";
        public const string ConfirmPassword = "Потвърдете парола";
        public const string Show = "Покажи";
        public const string DateGreaterThan = "Крайната дата трябва да е след началната дата.";
        public const string RequiredField = "{0} е задължително поле.";
        public const string RequiredFieldEmpty = "Задължително поле.";
        public const string InvalidEmail = "Невалиден имейл адрес.";
        public const string PasswordLenght = "Паролата {0} трябва да е между {2} и {1} символа.";
        public const string PasswordMatch = "Стойностите в полетата за парола не съвпадат.";
        public const string Amount = "Сума";
        public const string DueDate = "Крайна дата";
        public const string Paid = "Платено";

        public const string TotalStudents = "Общ брой студенти";
        public const string TotalPaidAmount = "Платени";
        public const string TotalAmount = "Въведени плащания";

        public const int MinStringLength = 1;
        public const int MinEmailLength = 5;
        public const int MaxEmailLength = 100;
        public const int MinPasswordLength = 5;
        public const int MaxPasswordLength = 100;
        public const int MaxTitleLength = 100;
        public const int MaxPaymentTitleLength = 250;
        public const int MaxFileNameLength = 250;
        public const int MaxAlbumDescriptionLength = 250;
        public const int MaxTeacherescriptionLength = 5000;
        public const int MaxFolderLength = 255;
        public const int CoverPhotoLength = 255;
        public const int MaxConactMessageLength = 2000;
        public const int MaxPostDescriptionLength = 2000;
        public const string PhoneNumberRegularExpression = @"\+[0-9]*";
        public const int MinPhoneNumberLength = 5;
        public const int MaxPhoneNumberLength = 20;
        public const int MaxNameLength = 100;
    }
}
