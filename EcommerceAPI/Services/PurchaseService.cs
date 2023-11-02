using AutoMapper;
using EcommerceAPI.Models.Publication;
using EcommerceAPI.Models.Publication.Dto;
using EcommerceAPI.Models.Purchase;
using EcommerceAPI.Models.Purchase.Dto;
using EcommerceAPI.Models.User;
using EcommerceAPI.Models.User.Dto;
using EcommerceAPI.Repositories;
using System.Net;
using System.Web.Http;

namespace EcommerceAPI.Services
{
    public class PurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;

        public PurchaseService(IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
        }


        public async Task<List<PurchasesDto>> GetAllByUserId(int id)
        {

            var lista = await _purchaseRepository.GetAllPurchases(p=>p.UserId==id);


            return _mapper.Map<List<PurchasesDto>>(lista);
        }

        public async Task<PurchaseDto> GetOneById(int id)
        {
            var purchase = await _purchaseRepository.GetOnePurchase(p=>p.PurchaseId==id);
            return _mapper.Map<PurchaseDto>(purchase);
        }

        public async Task<PurchaseDto> Create(CreatePurchaseDto createPurchaseDto)
        {
            Purchase purchase = new Purchase()
            {
                Amount = createPurchaseDto.Amount,
                UserId = createPurchaseDto.UserId
            };

            await _purchaseRepository.Add(purchase);
            return _mapper.Map<PurchaseDto>(purchase);

        }

        public async Task<PurchaseDto> UpdateById(int id, List<Publication> publications)
        {
            var purchase = await _purchaseRepository.GetOne(p => p.PurchaseId == id);

            if (purchase == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            purchase.Publications=publications;

            return _mapper.Map<PurchaseDto>(await _purchaseRepository.Update(purchase));
        }


    }
}
