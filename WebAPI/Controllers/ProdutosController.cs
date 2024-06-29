using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Entities.Entities;
using Entities.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProductServices _IProductService;
        private readonly IMapper _IMapper;

        public ProdutosController(IProductServices IProductService, IMapper IMapper)
        {
            _IProductService = IProductService;
            _IMapper = IMapper;
        }

        [Produces("application/json")]
        [HttpGet("/api/[controller]/Listar")]
        public List<ProductSupplierViewModel> Listar(int numeroPagina, int quantidadePorPagina)
        {
            var retorno = _IProductService.ListProdutoSupplier(numeroPagina, quantidadePorPagina);
            var retornoMap = _IMapper.Map<List<ProductSupplierViewModel>>(retorno);
            return retornoMap;
        }

        [Produces("application/json")]
        [HttpGet("/api/[controller]/BuscarPorCodigo")]
        public async Task<IActionResult> BuscarPorCodigo(int id)
        {
            var product = await _IProductService.GetEntityById(id);
            if (product == null) 
                return NotFound();

            var retornoMap = _IMapper.Map<ProductSupplierViewModel>(product);
            return Ok(retornoMap);
        }

        [Produces("application/json")]
        [HttpPost("/api/[controller]/Inserir")]
        public async Task<IActionResult> Inserir(ProductViewModel product)
        {
            try
            {
                if (product == null)
                    return BadRequest();

                var productMap = _IMapper.Map<Product>(product);
                
                var retorno = await _IProductService.Add(productMap);

                if (!retorno.Success)
                    return BadRequest(retorno.Message);

                return Accepted();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpPatch("/api/[controller]/Atualizar")]
        public async Task<IActionResult> Atualizar(int id, ProductViewModel product)
        {
            try
            {
                if (product == null || id == 0 || product.Id == 0 || id != product.Id)
                    return BadRequest();

                var productMap = _IMapper.Map<Product>(product);
                var retorno = await _IProductService.Update(productMap);

                if (!retorno.Success)
                    return BadRequest(retorno.Message);

                return Accepted();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpDelete("/api/[controller]/Deletar")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                var product = await _IProductService.GetEntityById(id);
                if(product == null) return NotFound();
                product.IsActive = false;
                var retorno = await _IProductService.Update(product);

                if (!retorno.Success)
                    return BadRequest(retorno.Message);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

    }
}
