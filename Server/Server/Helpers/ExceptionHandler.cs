using Server.Models;

namespace Server.Helpers
{
    public class ExceptionHandler : Exception
    {
        private string _Name;
        private string _Description;
        private string _Explanation;
        private string? _Time;

        public ExceptionHandler(string name = "", string description = "", string explanation = "")
        {
            _Name = name;
            _Description = description;
            _Explanation = explanation;

        }

        public class InvalidFileException : ExceptionHandler
        {

            public InvalidFileException(string explanation = "") : base()
            {
                DateTime Now = DateTime.Now;

                _Name = "InvalidFileException";
                _Description = "Invalid or empty file.";
                _Explanation = explanation;
                _Time = Now.ToString();

            }
        }


        // This is not used but could potentialy be, for now prevented with a check.
        public class CarNameExistsException : ExceptionHandler
        {

            public CarNameExistsException(string explanation = "") : base()
            {
                DateTime Now = DateTime.Now;

                _Name = "CarNameExistsException";
                _Description = "A car with this name already exists, duplicates are not allowed.";
                _Explanation = explanation;
                _Time = Now.ToString();

            }
        }

        // This is not used but could potentialy be, for now prevented with a check.
        public class ImageNotFoundException : ExceptionHandler
        {

            public ImageNotFoundException(string explanation = "") : base()
            {
                DateTime Now = DateTime.Now;

                _Name = "ImageNotFoundException";
                _Description = "This Image Doesnt Exist.";
                _Explanation = explanation;
                _Time = Now.ToString();

            }
        }

    }
}