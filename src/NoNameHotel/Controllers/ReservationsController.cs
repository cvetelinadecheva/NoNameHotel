using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoNameHotel.Data;
using NoNameHotel.Models;

namespace NoNameHotel.Controllers
{
    public class ReservationsController : Controller
    {
        private static int NumRooms;
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Reservations
        public IActionResult Index(string Reservations = "All" , string Room = "All", string Month = "All")
        {
            if (Reservations == "All" && Room == "All" && Month == "All")
            {
                IQueryable<Reservation> reserv = _context.Reservation.Include(x => x.Client);
                return View(reserv);
            }
            else if (Reservations == "All" && Room != "All" && Month == "All")
            {
                var r = _context.Room.Last(m => m.NumRoom.Equals(Room));            
                IQueryable<Reservation> reserv = _context.Reservation.Include(x => x.Client).Where(x => x.RoomId.Equals(r.Id));
                return View(reserv);
            }
            else if (Reservations != "All" && Room == "All" && Month == "All")
            {
                IQueryable<Reservation> reserv = _context.Reservation.Include(x => x.Client).Where(x => x.FromDate <= DateTime.Now.Date && x.ToDate>=DateTime.Now.Date);
                return View(reserv);
            }
            else if (Reservations == "All" && Room == "All" && Month != "All")
            {
                IQueryable<Reservation> reserv = _context.Reservation.Include(x => x.Client).Where(x => x.FromDate.Month.ToString() == Month || x.ToDate.Month.ToString() == Month);
                return View(reserv);
            }
       
            else //if (Reservations == "All" && Room != "All" && Month != "All")
            {
                var r = _context.Room.Last(m => m.NumRoom.Equals(Room));
                IQueryable<Reservation> reserv = _context.Reservation.Include(x => x.Client).Where(x => x.RoomId == r.Id && (x.FromDate.Month.ToString() == Month || x.ToDate.Month.ToString() == Month));
                return View(reserv);
            }


            //var result =  Mapper.Map<ReservationViewModel>(reserv); 
            //return View(result);

        }


        //// post: Reservations
        //public IActionResult DeleteReservations(IEnumerable<ReservationViewModel> model)
        //{
        //  var todelete = model.Where(o=>o.selected)
        //   delete

        //}


        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.SingleOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
      //  [Authorize]
        public IActionResult Create()
        {
            return View();
        }
      
        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id, int RoomId,int NumOfRooms , int Rooms)
          {           
                var oldRoom = await _context.Room.LastAsync(r => r.Id == RoomId);
                ViewBag.NumberOfAdults = oldRoom.NumberOfAdults;
                ViewBag.NumberOfChildren = oldRoom.NumberOfChildren;
                NumRooms = ViewBag.NumOfRooms = NumOfRooms;
                ViewBag.Rooms = Rooms;
          
                if (id == null)
                {
                    return NotFound();
                }

                var reservation = await _context.Reservation.SingleOrDefaultAsync(m => m.Id == id);
                if (reservation == null)
                {
                    return NotFound();
                }

                return View(reservation);
            
          }


        public async Task<IActionResult> AdminEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.SingleOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);

        }
        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FromDate,NumberOfAdults,NumberOfChildren,Status,ToDate,ClientId,RoomId")] Reservation reservation)
        {
            var oldRoom = await _context.Room.LastAsync(r => r.Id == reservation.RoomId);
            ViewBag.NumberOfAdults = oldRoom.NumberOfAdults;
            ViewBag.NumberOfChildren = oldRoom.NumberOfChildren;
            var client = await _context.Client.LastAsync(m => m.Id == reservation.ClientId);
            if (id != reservation.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                    try
                    {
                        _context.Update(reservation);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ReservationExists(reservation.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    int room = ValidateReservation(reservation);
                    if (room != 0)
                    {
                        _context.Entry(reservation).Property("RoomId").CurrentValue = room;
                        await _context.SaveChangesAsync();  
                    if(NumRooms<1)                  
                        return RedirectToAction("Edit", "Clients", new { id = client.Id });
                    else
                        return RedirectToAction("Index", "Categories", new { FromDate = reservation.FromDate, ToDate = reservation.ToDate, NumberOfRooms = ViewBag.NumOfRooms });
                }
                    else
                        ModelState.AddModelError(String.Empty, "The hotel is full!");
                
            }
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.SingleOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        public async Task<IActionResult> DeleteChecked(string ids)
        {
            if (ids != null)
            {
                var str = ids.Split(',');
                int[] idToDelete = str.Select(int.Parse).ToArray();
                var reservation = new Reservation();
                foreach (var i in idToDelete)
                {
                    reservation = await _context.Reservation.SingleOrDefaultAsync(m => m.Id == i);
                    _context.Reservation.Remove(reservation);

                }
                await _context.SaveChangesAsync();
            }
                return RedirectToAction("Index");
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservation.SingleOrDefaultAsync(m => m.Id == id);
            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }

        public int ValidateReservation([Bind("Id,FromDate,NumberOfAdults,NumberOfChildren,Status,ToDate,ClientId,RoomId")] Reservation reservation)
        {
            int roomnum = 0;
                var roomReservation = _context.Room.First(m => m.Id == reservation.RoomId);

                var rooms = from rm in _context.Room                                       
                            where rm.CategoryId.Equals(roomReservation.CategoryId)
                            select rm;

                var reservations = from rs in _context.Reservation
                                   join rm in _context.Room
                                   on rs.RoomId equals rm.Id
                                   where rm.CategoryId.Equals(roomReservation.CategoryId)
                                   select rs;

                foreach (var room in rooms)
                {
                    roomnum = room.Id;
                    foreach (var res in reservations)
                    {
                        if(!res.Equals(reservations.Last()))
                            if ((reservation.FromDate == res.FromDate
                                || reservation.FromDate == res.ToDate.AddDays(-1)
                                || reservation.ToDate == res.FromDate
                                || reservation.ToDate == res.ToDate.AddDays(-1))
                                && res.RoomId == room.Id)
                                roomnum = 0;
                            else if (((reservation.FromDate < res.FromDate && reservation.ToDate > res.ToDate.AddDays(-1))
                                    || (reservation.FromDate > res.FromDate && reservation.ToDate < res.ToDate.AddDays(-1))
                                    || (reservation.FromDate > res.FromDate && reservation.FromDate < res.ToDate.AddDays(-1))
                                    || (reservation.ToDate > res.FromDate && reservation.ToDate < res.ToDate.AddDays(-1)))
                                    && res.RoomId == room.Id)
                                roomnum = 0;
                            if (roomnum != 0 && res.RoomId == room.Id)
                            {                       
                                return roomnum=res.RoomId;                    
                            }

                    }
                    if (roomnum != 0)        
                        return roomnum;
                }
                return 0;
        }
    }
}
