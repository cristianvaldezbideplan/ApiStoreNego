﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ApiStore.Models;

public class Registro
    {

    public int Id { get; set; }
	public string? Usuario { get; set; }
	public string? Password { get; set; }
	public string? Email { get; set; }
	public string? Domicilio { get; set; }
	public string? Telefono { get; set; }
	public string? Celular { get; set; }
	public string? Provincia { get; set; }
	public string? Localidad { get; set; }
	public int	CodigoPostal { get; set; }
	public int IdRol { get; set; }
	public DateTime Fecha { get; set; }
	public string? Activo { get; set;}

}
