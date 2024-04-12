namespace ProductMiniApi.Repository.Abastract
{
    public interface IFileService
    {
        public Tuple<int, string> SaveImage(IFormFile imageFile);
        public Task DeleteImage(string imageFileName);
    }
}
