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


        public class ImageAddingException : ExceptionHandler
        {

            public ImageAddingException(string explanation = "") : base()
            {
                DateTime Now = DateTime.Now;

                _Name = "ImageAddingException";
                _Description = "Could not add image.";
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

        public class ModelStateException : ExceptionHandler
        {

            public ModelStateException(string explanation = "") : base()
            {
                DateTime Now = DateTime.Now;

                _Name = "ModelStateException";
                _Description = "Modelstate is not right.";
                _Explanation = explanation;
                _Time = Now.ToString();

            }
        }

        public class UnauthorizedUserException : ExceptionHandler
        {

            public UnauthorizedUserException(string explanation = "") : base()
            {
                DateTime Now = DateTime.Now;

                _Name = "UnauthorizedUserException";
                _Description = "User request to log in isnt authorized.";
                _Explanation = explanation;
                _Time = Now.ToString();

            }
        }

    }
}