using System.Text;
using EntityFrameworkCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreApp.Controllers{
  public class OgrenciController:Controller{

    private readonly DataContext _context;
    public OgrenciController(DataContext context){
       _context=context;
    }

    public async Task<IActionResult> Index(){
        var ogrenciler=await _context.Ogrenciler.ToListAsync();
        return View(ogrenciler);
    }

    public IActionResult Create(){
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Ogrenci model){
        _context.Ogrenciler.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int? id){
       if(id==null){
         return NotFound();
       }

       var ogrenci=await _context.Ogrenciler.Include(x=>x.KursKayitlari).ThenInclude(x=>x.Kurs).FirstOrDefaultAsync(x=>x.OgrenciId==id);
       if(ogrenci==null){
         return NotFound();
       }

       return View(ogrenci);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id,Ogrenci model){
        if(id != model.OgrenciId){

            return NotFound();

        }

        if(ModelState.IsValid){

          try
          {

            _context.Update(model);
            await _context.SaveChangesAsync();
          
          }
          catch(Exception){

            if(_context.Ogrenciler.Any(o=> o.OgrenciId != model.OgrenciId)){
              return NotFound();
            }else{
              throw;
            }

          }
          return RedirectToAction("Index");
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id){
      if(id==null){
        return NotFound();
      }

      var ogrenci=await _context.Ogrenciler.FindAsync(id);
      
      if(ogrenci==null){
        return NotFound();
      }

      return View(ogrenci);

    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm]int id){

      var ogrenci=await _context.Ogrenciler.FindAsync(id);
      if(ogrenci==null){
        return NotFound();
      }
      _context.Ogrenciler.Remove(ogrenci);
      await _context.SaveChangesAsync();
      return RedirectToAction("Index");

    }
  }
}