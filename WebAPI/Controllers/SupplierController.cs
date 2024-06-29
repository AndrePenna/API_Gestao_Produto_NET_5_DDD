using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierServices _ISupplierService;
        private readonly IMapper _IMapper;

        public SupplierController(IMapper iMapper, ISupplierServices iSupplierServices)
        {
            _IMapper = iMapper;
            _ISupplierService = iSupplierServices;
        }

        [Produces("application/json")]
        [HttpGet("/api/[controller]/Listar")]
        public async Task<List<SupplierViewModel>> Listar(int numeroPagina, int quantidadePorPagina)
        {
            var retorno = await _ISupplierService.List(numeroPagina, quantidadePorPagina);
            var retornoMap = _IMapper.Map<List<SupplierViewModel>>(retorno);
            return retornoMap;
        }

        [Produces("application/json")]
        [HttpGet("/api/[controller]/BuscarPorCodigo")]
        public async Task<IActionResult> BuscarPorCodigo(int id)
        {
            var supplier = await _ISupplierService.GetEntityById(id);
            if (supplier == null)
                return NotFound();

            var retornoMap = _IMapper.Map<SupplierViewModel>(supplier);
            return Ok(retornoMap);
        }

        [Produces("application/json")]
        [HttpPost("/api/[controller]/Inserir")]
        public async Task<IActionResult> Inserir(SupplierViewModel supplier)
        {
            try
            {
                if (supplier == null)
                    return BadRequest();

                var supplierMap = _IMapper.Map<Supplier>(supplier);

                var retorno = await _ISupplierService.Add(supplierMap);

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
        public async Task<IActionResult> Atualizar(int id, Supplier supplier)
        {
            try
            {
                if (supplier == null || id == 0 || supplier.Id == 0 || id != supplier.Id)
                    return BadRequest();

                var retorno = await _ISupplierService.Update(supplier);

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
        [HttpDelete("/api/[controller]/Deletar/")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                var supplier = await _ISupplierService.GetEntityById(id);

                if (supplier == null) return NotFound();

                var retorno = await _ISupplierService.Delete(supplier);

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
