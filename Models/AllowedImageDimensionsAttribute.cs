using System.ComponentModel.DataAnnotations;

namespace V._3._0.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AllowedImageDimensionsAttribute : ValidationAttribute
    {
        private readonly int _minWidth;
        private readonly int _maxWidth;
        private readonly int _minHeight;
        private readonly int _maxHeight;

        public AllowedImageDimensionsAttribute(int minWidth, int maxWidth, int minHeight, int maxHeight)
        {
            _minWidth = minWidth;
            _maxWidth = maxWidth;
            _minHeight = minHeight;
            _maxHeight = maxHeight;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                using (var image = System.Drawing.Image.FromStream(file.OpenReadStream()))
                {
                    var width = image.Width;
                    var height = image.Height;

                    if (width < _minWidth || width > _maxWidth || height < _minHeight || height > _maxHeight)
                    {
                        return new ValidationResult($"Image dimensions must be between {_minWidth}px - {_maxWidth}px in width and {_minHeight}px - {_maxHeight}px in height.");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }

}