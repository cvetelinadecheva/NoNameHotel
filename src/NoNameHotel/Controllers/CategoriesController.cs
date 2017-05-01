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
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private static int NumOfRooms , Rooms;
        private static DateTime FromD, ToD;
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Categories
        public async Task<IActionResult> Index(DateTime FromDate,DateTime ToDate,int NumberOfRooms)
        {
            if (FromDate.Date == DateTime.Parse("1/1/0001"))
            {
                Rooms = 0;
                ViewBag.Error = "";
                var client = await _context.Client.SingleOrDefaultAsync(m => m.Email == "");
                if (client != null)
                {
                    var res = from r in _context.Reservation
                              where r.ClientId.Equals(client.Id)
                              select r;
                    foreach (var r in res)
                    {                      
                        _context.Reservation.Remove(r);
                       
                    }
                    await _context.SaveChangesAsync();
                }
                return View(await _context.Category.ToListAsync());
            }
            else if (FromDate.Date <= DateTime.Today)
                ViewBag.Error = "Invalid start date! Start date must be after today";
            else if (FromDate.Date == ToDate.Date)
                ViewBag.Error = "Invalid dates! Start date and end date must be different!";
            else if (ToDate.Date <= DateTime.Today)
                ViewBag.Error = "Invalid end date! End date must be after today";
            else
            {
                if (NumberOfRooms != 0)
                {
                    Rooms = NumberOfRooms;
                    NumOfRooms = NumberOfRooms;
                }
                if (NumOfRooms == 0)
                    Rooms = 0;
                FromD = FromDate.Date;
                ToD = ToDate.Date;
                ViewBag.Rooms = Rooms;
                ViewBag.NumOfRooms = NumOfRooms;
                ViewBag.From = FromDate.Date;
                ViewBag.To = ToDate.Date;
            }
           List <Category> cats2 = new List<Category>();
           var cats = from m in _context.Category
                         select m;
            foreach(var c in cats)
            {
                var roomsCategory = from rm in _context.Room
                                   where rm.CategoryId.Equals(c.Id)
                                   select rm;
                var reservationsCategory = from rs in _context.Reservation
                                          join rm in _context.Room
                                          on rs.RoomId equals rm.Id
                                          where rm.CategoryId.Equals(c.Id)
                                          select rs;
                int Economy = roomsCategory.Count();

                foreach (var room in roomsCategory)
                {
                    foreach (var res in reservationsCategory)
                    {
                        if ((FromD == res.FromDate
                            || FromD == res.ToDate.AddDays(-1)
                            || ToD == res.FromDate
                            || ToD == res.ToDate.AddDays(-1))
                            && res.RoomId == room.Id)
                            Economy--;
                        else if (((FromD < res.FromDate && ToD > res.ToDate.AddDays(-1))
                                || (FromD > res.FromDate && ToD < res.ToDate.AddDays(-1))
                                || (FromD > res.FromDate && FromD < res.ToDate.AddDays(-1))
                                || (ToD > res.FromDate && ToD < res.ToDate.AddDays(-1)))
                                && res.RoomId == room.Id)
                            Economy--;
                    }
                    
                }
                if (Economy > 0)
                    cats2.Add(await _context.Category.SingleOrDefaultAsync(m => m.Id == c.Id));
            }

            return View(cats2);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.SingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Picture,Price,Title")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.SingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Picture,Price,Title")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.SingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Category.SingleOrDefaultAsync(m => m.Id == id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Reservation(int? id)
        {       
            var room = await _context.Room.FirstAsync(m => m.CategoryId == id);
            var reservation = await _context.Reservation.LastOrDefaultAsync(m => m.RoomId == 6);
                if (reservation != null)
                {
                    reservation.RoomId = room.Id;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var client = await _context.Client.SingleOrDefaultAsync(m => m.Email == "");
                    if (client == null)
                    {
                        client = new Client
                        {
                            FirstName = "",
                            LastName = "",
                            TelNumber = "",
                            Email = ""
                        };
                        _context.Add(client);
                        await _context.SaveChangesAsync();

                    }
                if (NumOfRooms == 0)
                {
                    reservation = new Reservation
                    {
                        FromDate = DateTime.Now,
                        ToDate = DateTime.Now,
                        NumberOfAdults = 1,
                        NumberOfChildren = 0,
                        Status = Status.New,
                        ClientId = client.Id,
                        RoomId = room.Id
                    };
                }
                else
                {
                    reservation = new Reservation
                    {
                        FromDate = FromD,
                        ToDate = ToD,
                        NumberOfAdults = 0,
                        NumberOfChildren = 0,
                        Status = Status.New,
                        ClientId = client.Id,
                        RoomId = room.Id
                    };
                    int roomNum = ValidateReservation(reservation);
                    reservation.RoomId = roomNum;
                    NumOfRooms--;
                }
                _context.Add(reservation);
                    await _context.SaveChangesAsync();
                
            }
                return RedirectToAction("Edit", "Reservations", new { id = reservation.Id , RoomId = reservation.RoomId , NumOfRooms = NumOfRooms , Rooms = Rooms});
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
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
                    
                        if ((reservation.FromDate.Date == res.FromDate.Date
                            || reservation.FromDate.Date == res.ToDate.Date.AddDays(-1)
                            || reservation.ToDate.Date == res.FromDate.Date
                            || reservation.ToDate.Date == res.ToDate.Date.AddDays(-1))
                            && res.RoomId == room.Id)
                            roomnum = 0;
                        else if (((reservation.FromDate.Date < res.FromDate.Date && reservation.ToDate.Date > res.ToDate.Date.AddDays(-1))
                                || (reservation.FromDate.Date > res.FromDate.Date && reservation.ToDate.Date < res.ToDate.Date.AddDays(-1))
                                || (reservation.FromDate.Date > res.FromDate.Date && reservation.FromDate.Date < res.ToDate.Date.AddDays(-1))
                                || (reservation.ToDate.Date > res.FromDate.Date && reservation.ToDate.Date < res.ToDate.Date.AddDays(-1)))
                                && res.RoomId == room.Id)
                            roomnum = 0;
                    if (roomnum != 0 && res.RoomId == room.Id)
                    {
                        return roomnum = res.RoomId;
                    }

                }
                if (roomnum != 0 )//&& roomnum != 6)
                    return roomnum;
            }
            return 0;
        }


    }
}
