namespace ApiStore.Models.Dto;

public class CrearProductoDto
{
    public string? NumeroProducto { get; set; }
    public string? NombreProducto { get; set; }
    public string? Medidas { get; set; }
    public int? Cantidad { get; set; }
    public int? Peso { get; set; }
    public decimal? Precio { get; set; }
    public string? TipoDeMoneda { get; set; }
    public string? Descripcion { get; set; }
    public int? Stock { get; set; }
    public string? Imagen { get; set; }
    public string? RutaImagen { get; set; }
    public string? RutaImagenLocal { get; set; }
    public int Categoria { get; set; }

}
