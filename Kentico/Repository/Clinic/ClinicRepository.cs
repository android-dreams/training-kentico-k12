﻿using System.Linq;
using CMS.DocumentEngine.Types.Training;
using Kentico.Dto.Clinic;
using Kentico.Services.Query;

namespace Kentico.Repository.Clinic
{
    public class ClinicRepository : BaseRepository, IClinicRepository
    {

        public ClinicRepository(IDocumentQueryService documentQueryService) : base(documentQueryService)
        {
        }

        public ClinicDto GetClinic()
        {
            return DocumentQueryService.GetDocuments<MedioClinicSection>()
                .TopN(1)
                .Columns("MedioClinicName", "MedioClinicStreet", "MedioClinicCity", "MedioClinicCountry",
                    "MedioClinicZipCode", "MedioClinicPhoneNumber", "MedioClinicEmail", "DocumentID")
                .OrderByAscending("NodeOrder")
                .ToList()
                .Select(m =>
                {
                    var splitCountry = m.MedioClinicCountry.Split(';');

                    string country;
                    string state = null;
                    if (splitCountry.Length == 2)
                    {
                        country = splitCountry[0];
                        state = splitCountry[1];
                    }
                    else
                    {
                        country = m.MedioClinicCountry;
                    }

                    return new ClinicDto()
                    {
                        Name = m.MedioClinicName,
                        City = m.MedioClinicCity,
                        Street = m.MedioClinicStreet,
                        Country = country,
                        State = state,
                        Email = m.MedioClinicEmail,
                        PhoneNumber = m.MedioClinicPhoneNumber
                    };
                })
                .FirstOrDefault();
        }
    }
}