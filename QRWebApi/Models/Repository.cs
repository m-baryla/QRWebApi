using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace QRWebApi.Models
{
    public class Repository
    {
        private readonly QRAppDBContext _context;

        public Repository(QRAppDBContext context)
        {
            _context = context;
        }

        public async Task<List<DictEmailAdress>> GetDictEmailAdresses()
        {
            return await _context.DictEmailAdresses.ToListAsync();
        }
        public async Task<List<Wiki>> GetWikis()
        {
            return await _context.Wikis.ToListAsync();
        }
        public async Task<List<DictEquipment>> GetDictEquipments()
        {
            return await _context.DictEquipments.ToListAsync();
        }
        public async Task<List<DictLocation>> GetDictLocations()
        {
            return await _context.DictLocations.ToListAsync();
        }
        public async Task<List<DictTicketType>> GetDictTicketTypes()
        {
            return await _context.DictTicketTypes.ToListAsync();
        }
        public async Task<DictTicketType> GetDictTicketType(int id)
        {
            return await _context.DictTicketTypes.FindAsync(id);
        }
        public async Task<DictPriority> GetDictPriority(int id)
        {
            return await _context.DictPriorities.FindAsync(id);
        }
        public async Task<List<DictStatus>> GetDictStatus()
        {
            return await _context.DictStatus.ToListAsync();
        }
        public async Task<List<DictPriority>> GetDictPriorities()
        {
            return await _context.DictPriorities.ToListAsync();
        }
        public async Task<List<WikiDetails>> GetWikiDetail()
        {
            var query = (from w in _context.Wikis
                join l in _context.DictLocations on w.IdLocation equals l.Id
                join e in _context.DictEquipments on w.IdEquipment equals e.Id
                select new WikiDetails
                {
                    LocationName = l.LocationName,
                    EquipmentName = e.EquipmentName,
                    Topic = w.Topic,
                    Description = w.Description,
                    Photo = w.Photo
                }).ToListAsync();

            return await query;
        }
        public async Task<List<TicketsDetails>> TicketsHistoriesDetails()
        {
            var query = (from h in _context.Tickets
                         join a in _context.DictEmailAdresses on h.IdEmailAdress equals a.Id
                         join e in _context.DictEquipments on h.IdEquipment equals e.Id
                         join l in _context.DictLocations on h.IdLocation equals l.Id
                         join s in _context.DictStatus on h.IdStatus equals s.Id
                         join t in _context.DictTicketTypes on h.IdTicketType equals t.Id
                         join p in _context.DictPriorities on h.IdPriority equals p.Id
                         select new TicketsDetails
                         {
                             Id = h.Id,
                             UserName = h.UserName,
                             Topic = h.Topic,
                             Description = h.Description,
                             Photo = h.Photo,
                             LocationName = l.LocationName,
                             EquipmentName = e.EquipmentName,
                             EmailAdress = a.EmailAdressNotify,
                             Status = s.Status,
                             Priority = p.PriorityType,
                             TicketType = t.Type

                         }).ToListAsync();

            return await query;
        }
        public async Task<List<DictTicketTypeDetail>> GetDictTicketTypesAllNotActive()
        {
            var query = (from a in _context.DictStatus
                join b in _context.Tickets on a.Id equals b.IdStatus
                where a.Status == "Not Active"
                group a by a.Status into g
                select new DictTicketTypeDetail
                {
                    Type = g.Key,
                    Count = g.Count()
                }).ToListAsync();

            return await query;
        }
        public async Task<EmailSenderConfig> GetEmailConfig(string pass)
        {
            var query = (from h in _context.EmailSenderConfigs
                select new EmailSenderConfig
                {
                    MailFrom = h.MailFrom,
                    MailHost = h.MailHost,
                    EmailUser = h.EmailUser,
                    EmailPassword = Encoding.UTF8.GetString(Convert.FromBase64String(pass))
                }).SingleOrDefaultAsync();

            return await query;
        }

        public async Task<List<DictTicketTypeDetail>> GetDictTicketTypesNotActive()
        {
            var query = (from a in _context.DictTicketTypes
                join b in _context.Tickets on a.Id equals b.IdTicketType
                join s in _context.DictStatus on b.IdStatus equals s.Id
                where s.Status == "Not Active"
                group a by a.Type into g
                select new DictTicketTypeDetail
                {
                    Type = g.Key,
                    Count = g.Count()
                }).ToListAsync();

            return await query;
        }
        public async Task<List<DictTicketTypeDetail>> GetDictTicketTypesAllActive()
        {
            var query = (from a in _context.DictStatus
                join b in _context.Tickets on a.Id equals b.IdStatus
                where a.Status == "Active"
                group a by a.Status into g
                select new DictTicketTypeDetail
                {
                    Type = g.Key,
                    Count = g.Count()
                }).ToListAsync();

            return await query;
        }

        public async Task<List<DictTicketTypeDetail>> GetDictTicketTypesActive()
        {
            var query = (from a in _context.DictTicketTypes
                join b in _context.Tickets on a.Id equals b.IdTicketType
                join s in _context.DictStatus on b.IdStatus equals s.Id
                where s.Status == "Active"
                group a by a.Type into g
                select new DictTicketTypeDetail
                {
                    Type = g.Key,
                    Count = g.Count()
                }).ToListAsync();

            return await query;
        }
        public async Task<DictLocation> PostDictLocation(DictLocation dictLocation)
        {
            _context.DictLocations.Add(dictLocation);
            await _context.SaveChangesAsync();
            return dictLocation;
        }
        public async Task<DictEquipment> PostDictEquipment(DictEquipment dictEquipment)
        {
            _context.DictEquipments.Add(dictEquipment);
            await _context.SaveChangesAsync();
            return dictEquipment;
        }
        public async Task<TicketsDetails> PostTicket(TicketsDetails ticket)
        {
            _context.Tickets.Add(new Ticket()
            {
                UserName = ticket.UserName,
                IdPriority = _context.DictPriorities.Where(p => p.PriorityType == ticket.Priority).Select(p => p.Id).First(),
                IdTicketType = _context.DictTicketTypes.Where(t => t.Type == ticket.TicketType).Select(t => t.Id).First(),
                Topic = ticket.Topic,
                Description = ticket.Description,
                Photo = ticket.Photo,
                IdLocation = _context.DictLocations.Where(l => l.LocationName == ticket.LocationName).Select(l => l.Id).First(),
                IdEquipment = _context.DictEquipments.Where(e => e.EquipmentName == ticket.EquipmentName).Select(e => e.Id).First(),
                IdStatus = _context.DictStatus.Where(s => s.Status == ticket.Status).Select(s => s.Id).First(),
                IdEmailAdress = _context.DictEmailAdresses.Where(e => e.EmailAdressNotify == ticket.EmailAdress).Select(e => e.Id).First()
            });
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<WikiDetails> PostWiki(WikiDetails wiki)
        {
            _context.Wikis.Add(new Wiki()
            {
                Topic = wiki.Topic,
                Description = wiki.Description,
                Photo = wiki.Photo,
                IdLocation = _context.DictLocations.Where(l => l.LocationName == wiki.LocationName).Select(l => l.Id).First(),
                IdEquipment = _context.DictEquipments.Where(e => e.EquipmentName == wiki.EquipmentName).Select(e => e.Id).First(),
            });
            await _context.SaveChangesAsync();
            return wiki;
        }
        public async Task<DictEmailAdress> PostDictEmailAdress(DictEmailAdress dictEmailAdress)
        {
            _context.DictEmailAdresses.Add(dictEmailAdress);
            await _context.SaveChangesAsync();
            return dictEmailAdress;
        }

        public async Task PutTicket(int id, TicketsDetails ticket)
        {
            var _ticket = new Ticket()
            {
                Id = ticket.Id,
                UserName = ticket.UserName,
                Topic = ticket.Topic,
                Description = ticket.Description,
                Photo = ticket.Photo,
                IdLocation = _context.DictLocations.Where(l => l.LocationName == ticket.LocationName).Select(l => l.Id).First(),
                IdEquipment = _context.DictEquipments.Where(e => e.EquipmentName == ticket.EquipmentName).Select(e => e.Id).First(),
                IdStatus = _context.DictStatus.Where(s => s.Status == ticket.Status).Select(s => s.Id).First(),
                IdEmailAdress = _context.DictEmailAdresses.Where(e => e.EmailAdressNotify == ticket.EmailAdress).Select(e => e.Id).First(),
                IdPriority = _context.DictPriorities.Where(p => p.PriorityType == ticket.Priority).Select(p => p.Id).First(),
                IdTicketType = _context.DictTicketTypes.Where(t => t.Type == ticket.TicketType).Select(t => t.Id).First()
            };

            _context.Entry(_ticket).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
