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

        public class DeleteFailedException : ExceptionHandler
        {

            public DeleteFailedException(string explanation = "") : base()
            {
                DateTime Now = DateTime.Now;

                _Name = "DeleteFailedException";
                _Description = "Delete failed.";
                _Explanation = explanation;
                _Time = Now.ToString();

            }
        }

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

        public class ImageGetFailedException : ExceptionHandler
        {

            public ImageGetFailedException(string explanation = "") : base()
            {
                DateTime Now = DateTime.Now;

                _Name = "ImageGetFailedException";
                _Description = "Failed to get the image.";
                _Explanation = explanation;
                _Time = Now.ToString();

            }
        }

        public class CarAddingException : ExceptionHandler
        {

            public CarAddingException(string explanation = "") : base()
            {
                DateTime Now = DateTime.Now;

                _Name = "CarAddingException";
                _Description = "Failed to add a car.";
                _Explanation = explanation;
                _Time = Now.ToString();

            }
        }

        public class CarUpdatingException : ExceptionHandler
        {

            public CarUpdatingException(string explanation = "") : base()
            {
                DateTime Now = DateTime.Now;

                _Name = "CarUpdatingException";
                _Description = "Updating the values of a car failed.";
                _Explanation = explanation;
                _Time = Now.ToString();

            }
        }

        public class CarGetException : ExceptionHandler
        {

            public CarGetException(string explanation = "") : base()
            {
                DateTime Now = DateTime.Now;

                _Name = "CarGetException";
                _Description = "Failed to get car.";
                _Explanation = explanation;
                _Time = Now.ToString();

            }
        }

        public class NotFoundInDbException : ExceptionHandler
        {

            public NotFoundInDbException(string explanation = "") : base()
            {
                DateTime Now = DateTime.Now;

                _Name = "CarGetException";
                _Description = "Failed to get car.";
                _Explanation = explanation;
                _Time = Now.ToString();

            }
        }

    }
}