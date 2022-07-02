using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PosEcommerce.Models;
using System.Threading.Tasks;

namespace PosEcommerce.Controllers
{
    public class CategoryController : Controller
    {
        List<CategoryModel> all = new List<CategoryModel>();
        public List<int> idsList = new List<int>();
        public async Task<ActionResult> Index(int? catId)
        {
            CategoryModel category = new CategoryModel();
          //  List<CategoryModel> all = new List<CategoryModel>();

            //List<CategoryModel> catPathList = new List<CategoryModel>();
            ItemModel item = new ItemModel();
            List<ItemModel> allitems = new List<ItemModel>();
            all = await category.GetAllCategories();
            allitems = await item.GetAllItems();
            // List<CategoryModel> categoryList = new List<CategoryModel>();
            if (catId == 0 || catId == null)
            {
                ViewBag.categoryList = all.Where(x => x.parentId == 0).ToList();
                ViewBag.itemList = allitems;
                ViewBag.catPathList =  GetCategoryPath(0);
                ViewBag.idsList = idsList.ToList();
            }
            else
            {
                ViewBag.categoryList = all.Where(x => x.parentId == catId).ToList();

                ViewBag.catPathList =  GetCategoryPath((int)catId);
              GetCategoriesOfparent((int)catId);//fill idsList
                ViewBag.idsList = idsList.ToList();//4test
                ViewBag.itemList = allitems.Where(x => idsList.Contains((int)x.categoryId)).ToList();
            
                //List<int> catIds = new List<int>();
                //foreach (CategoryModel row in ViewBag.catPathList)
                //{
                //    catIds.Add(row.categoryId);
                //}
                //
            }

            //int i = 0;
            //           foreach(var row in ViewBag.categoryList)
            //            {

            //                var image = row.image;
            //                if (image != null && image.ToString() != "")
            //                {
            //                    //if (i<2)
            //                    {
            //var imageArr = await category.downloadImage(image);
            //                        row.notes = imageArr;
            //                    }

            //                    i++;
            //                }
            //            }
            return View(ViewBag.categoryList);
        }



     
        public List<CategoryModel> GetCategoriesroot(int parentId)
        {
            var categoriesList = all.Where(x => x.parentId == parentId && x.isActive == 1).ToList();
            return categoriesList;
        }
        public List<CategoryModel> GetsonCategories(List<CategoryModel> mainList)
        {

            foreach (CategoryModel row in mainList)
            {
                row.childCategories = GetSonsbyParentId(row.categoryId);
                idsList.Add(row.categoryId);
            }

            return mainList;
        }
        public List<CategoryModel> GetSonsbyParentId(int? parentId)
        {

            var categoriesList = all.Where(x => x.parentId == parentId && x.isActive == 1).ToList().OrderBy(x => x.createDate).ToList();


            foreach (CategoryModel rowch in categoriesList)
            {
                // rowch.childCategories = GetSonsbyParentId(rowch.categoryId);
                GetSonsbyParentId(rowch.categoryId);
                idsList.Add(rowch.categoryId);
            }
            return categoriesList;

        }

        public List<int> GetCategoriesOfparent(int categoryId)
        {
            try
            {

                //    activeCategories = all;

                List<CategoryModel> mainList = new List<CategoryModel>();
                idsList.Add(categoryId);
                mainList = GetCategoriesroot(categoryId);
                mainList = GetsonCategories(mainList);

                return idsList;
            }
            catch (Exception ex)
            {
                idsList = new List<int>();
                return idsList = new List<int>();

            }
        }

        public List<CategoryModel> GetCategoryPath(int categoryId)
        {


            List<CategoryModel> treecat = new List<CategoryModel>();
            int parentid = categoryId; // if want to show the last category 
                    while (parentid > 0)
                    {
                        CategoryModel tempcate = new CategoryModel();
                CategoryModel category = all.Where(c => c.categoryId == parentid)
                            .Select(p => new CategoryModel {
                             categoryId=   p.categoryId,
                                name=  p.name,
                                categoryCode= p.categoryCode,
                                //p.createDate,
                                //p.createUserId,
                                //p.details,
                                image=  p.image,
                                //p.notes,
                                parentId= p.parentId,
                                //p.taxes,
                                //p.fixedTax ,
                                //p.updateDate,
                                //p.updateUserId,
                            }).FirstOrDefault();


                        tempcate.categoryId = category.categoryId;

                        tempcate.name = category.name;
                        tempcate.categoryCode = category.categoryCode;
                        //tempcate.createDate = category.createDate;
                        //tempcate.createUserId = category.createUserId;
                        //tempcate.details = category.details;
                        tempcate.image = category.image;
                        //tempcate.notes = category.notes;
                        tempcate.parentId = category.parentId;
                        //tempcate.taxes = category.taxes;
                        //tempcate.fixedTax = category.fixedTax;
                        //tempcate.updateDate = category.updateDate;
                        //tempcate.updateUserId = category.updateUserId;


                        parentid = (int)tempcate.parentId;

                        treecat.Add(tempcate);

                    }
                    treecat.Reverse();
                    return treecat;

               


             
        }

    }

}
