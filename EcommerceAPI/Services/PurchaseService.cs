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
        private readonly IPublicationRepository _publicationRepository;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public PurchaseService(IPurchaseRepository purchaseRepository, IMapper mapper, IPublicationRepository publicationRepository,ApplicationDbContext db)
        {
            _purchaseRepository = purchaseRepository;
            _publicationRepository = publicationRepository;
            _db = db;
            _mapper = mapper;
        }


        public async Task<List<PurchasesDto>> GetAllByUserId(int id)
        {

            var lista = await _purchaseRepository.GetAllPurchases(p=>p.UserId==id);


            return _mapper.Map<List<PurchasesDto>>(lista);
        }

        public async Task<List<PurchasesDto>> GetAllBySelllerId(int id)
        {

            var lista = await _purchaseRepository.GetAllPurchases(p => p.SellerId == id);


            return _mapper.Map<List<PurchasesDto>>(lista);
        }

        public async Task<PurchaseDto> GetOneById(int id)
        {
            var purchase = await _purchaseRepository.GetOnePurchase(p=>p.PurchaseId==id);

            if (purchase == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return _mapper.Map<PurchaseDto>(purchase);
        }

        public async Task<PurchaseDto> Create(CreatePurchaseDto createPurchaseDto,List<Publication> publications)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                   
                    Purchase purchase = new Purchase()
                    {
                        Amount = createPurchaseDto.Amount,
                        UserId = createPurchaseDto.UserId,
                        SellerId = createPurchaseDto.SellerId,
                        Publications=publications
                    };

                   

                    
                    foreach (int id in createPurchaseDto.PublicationsIds)
                    {
                        Publication publication = await _publicationRepository.GetOne(p => p.PublicationId == id);
                        if (publication.Stock == 1)
                        {
                            publication.IsPaused = true;
                            publication.Stock = 0;
                        }
                        else
                        {
                            publication.Stock = publication.Stock - 1;
                        }
                        await _publicationRepository.Update(publication);
                    }

                    
                    await _purchaseRepository.Add(purchase);

                    
                    transaction.Commit();

                    return _mapper.Map<PurchaseDto>(purchase);
                }
                catch (Exception ex)
                {
                    
                    transaction.Rollback();

                    throw new Exception(ex.Message);
                }
            }
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

        public async Task<PurchaseDto> UpdateWasDeliveredById(int id)
        {
            var purchase = await _purchaseRepository.GetOne(p => p.PurchaseId == id);

            if (purchase == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            purchase.WasDelivered = true;

            return _mapper.Map<PurchaseDto>(await _purchaseRepository.Update(purchase));
        }


    }
}
