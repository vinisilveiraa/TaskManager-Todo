using Microsoft.AspNetCore.Mvc;

namespace ToDoApi.Services
{
    public class FileService
    {
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
        private const long MaxFileSize = 5 * 1024 * 1024;

        // iformfile e uma abstracao pra representar arquivos enviados em uma requisicao http, normalmente atraves de multipart/form-data
        // ele fornece informacoes como .filename .length .contenttype e tambem permite ler o conteudo do arquivo atraves de streams
        public async Task<string> SaveAvatarAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Nenhum arquivo enviado.");

            if (file.Length > MaxFileSize)
                throw new ArgumentException("A imagem deve ter no máximo 5 MB.");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant(); // pega o tipo de arquivo

            if (!_allowedExtensions.Contains(extension))
                throw new ArgumentException("Tipo de arquivo não permitido.");

            // directory e uma classe do .net pra trabalhar com diretorios/pastas
            // nesse caso ele ta pegando o diretorio atual, no caso /ToDoApi
            // tambem passando o resto das pastas, ficando /ToDoApi/wwwroot/uploads/avatars
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(),
               "wwwroot", // diretorio normalmente usado para arquivos estaticos e publicos
               "uploads",
               "avatars"
               );

            Directory.CreateDirectory(uploadsPath); // isso garante que a pasta exista, se n existir cria

            var fileName = $"{Guid.NewGuid()}{extension}"; // gera um nome de arquivo único usando um GUID - globally unique identifier + a extension salva anteriormente
            var filePath = Path.Combine(uploadsPath, fileName); // combina duas strings de caminho em um único caminho
            await using var stream = new FileStream(filePath, FileMode.Create); // cria um stream de arquivo para escrever o arquivo no caminho especificado


            await file.CopyToAsync(stream); // copia o arquivo pra stream definida

            return $"/uploads/avatars/{fileName}"; // retorna o caminho relativo do arquivo salvo
        }

        public async Task<IActionResult> DeleteAvatarAsync(string avatarPath)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", avatarPath.TrimStart('/'));

            if (!File.Exists(filePath))
                return new NotFoundObjectResult("Arquivo não encontrado.");
            try
            {
                File.Delete(filePath);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Erro ao deletar o arquivo: {ex.Message}");
            }
        }
    }
}
