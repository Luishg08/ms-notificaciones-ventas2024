using Microsoft.AspNetCore.Mvc;
using ms_notificaciones.Models;
using RestSharp;
using RestSharp.Authenticators;


namespace ms_notificaciones.Controllers;

[ApiController]
[Route("[controller]")]

public class NotificacionesController : ControllerBase
{   
    [HttpPost]
    [Route("correo")]
    public async Task<ActionResult> EnviarCorreo(ModeloCorreo datos)
    {
        Console.WriteLine("Enviando correo");
        var options = new RestClientOptions
        {
        BaseUrl = new Uri("https://api.mailgun.net/v3"),    
        Authenticator = new HttpBasicAuthenticator("api", "MAILGUN_API_KEY")
        };
        using var client = new RestClient(options);		
        RestRequest request = new RestRequest ();
		request.AddParameter ("domain", "", ParameterType.UrlSegment);
		request.Resource = "{}/messages";
		request.AddParameter ("from", "Mi nombre <>");
		request.AddParameter ("to", datos.correoDestino);
		request.AddParameter ("to", datos.correoDestino);
		request.AddParameter ("subject", datos.asuntoCorreo);
		request.AddParameter ("text", datos.contenidoCorreo);
		request.Method = Method.Post;
		var respuesta = client.Execute(request);
        if(respuesta.StatusCode == System.Net.HttpStatusCode.OK){
            return Ok("Correo enviado");
        }else{
            return BadRequest("Error al enviar el correo");
        }
    }

    [Route("sms")]
    [HttpGet]
    public Task<ActionResult> mostrarMensaje(){
        
        return Task.FromResult<ActionResult>(Ok("Mensaje de prueba"));
        
    }
}