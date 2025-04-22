using System.Text;
using System.Threading.Tasks;
using EntityFrameworkCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreApp.Controllers{
    
 public class OgretmenController:Controller{

   private readonly DataContext _context;
   public OgretmenController(DataContext context){
     _context=context;
   }

   public async Task<IActionResult> Index(){
     return View(await _context.Ogretmenler.ToListAsync());
   }

   public IActionResult Create(){
     return View();
   }

   [HttpPost]
   public async Task<IActionResult> Create(Ogretmen model){
     _context.Ogretmenler.Add(model);
     await _context.SaveChangesAsync();
     return RedirectToAction("Index");
   }
   

   [HttpGet]
    public async Task<IActionResult> Edit(int? id){
       if(id==null){
         return NotFound();
       }

       var ogretmen=await _context.Ogretmenler.Include(x=>x.Kurslar).FirstOrDefaultAsync(x=>x.OgretmenId==id);
       if(ogretmen==null){
         return NotFound();
       }

       return View(ogretmen);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id,Ogretmen model){
        if(id != model.OgretmenId){

            return NotFound();

        }

        if(ModelState.IsValid){

          try
          {

            _context.Update(model);
            await _context.SaveChangesAsync();
          
          }
          catch(Exception){

            if(_context.Ogretmenler.Any(o=> o.OgretmenId != model.OgretmenId)){
              return NotFound();
            }else{
              throw;
            }

          }
          return RedirectToAction("Index");
        }

        return View(model);
    }
 }

}