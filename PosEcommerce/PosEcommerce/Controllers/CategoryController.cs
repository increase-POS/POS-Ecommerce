﻿using System;
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
     //   public List<int> idsList = new List<int>();
        public async Task<ActionResult> Index(int? catId)
        {
            CategoryModel category = new CategoryModel();
            CategoryModel currentCategory = new CategoryModel();
              List<int> idsList = new List<int>();
        List<CategoryModel> categoryList = new List<CategoryModel>();
            //  List<CategoryModel> all = new List<CategoryModel>();

            //List<CategoryModel> catPathList = new List<CategoryModel>();
            ItemModel item = new ItemModel();
            List<ItemModel> allitems = new List<ItemModel>();
            all = await category.GetAllCategories();
            allitems = await item.GetAllItems();
            // List<CategoryModel> categoryList = new List<CategoryModel>();
            if (catId == 0 || catId == null)
            {
                categoryList=all.Where(x => x.parentId == 0).ToList();
              
                ViewBag.itemList = allitems;
                ViewBag.catPathList =  GetCategoryPath(0);
                currentCategory.categoryId = 0;
                currentCategory.name = "Categories";

             //   ViewBag.idsList = idsList.ToList();

            }
            else
            {
                categoryList = all.Where(x => x.parentId == catId).ToList();
               

                ViewBag.catPathList =  GetCategoryPath((int)catId);
                idsList= GetCategoriesOfparent((int)catId, idsList);//fill idsList
               // ViewBag.idsList = idsList.ToList();//4test
                ViewBag.itemList = allitems.Where(x => idsList.Contains((int)x.categoryId)).ToList();
                currentCategory = all.Where(C => C.categoryId == (int)catId).FirstOrDefault();

                //List<int> catIds = new List<int>();
                //foreach (CategoryModel row in ViewBag.catPathList)
                //{
                //    catIds.Add(row.categoryId);
                //}
                //
            }
            List<int> idsListitem =  new List<int>();
            int catidcurrent = catId == null ? 0 :(int) catId;

            foreach (CategoryModel row in categoryList)
            {
                idsListitem = new List<int>();
                 idsListitem = GetCategoriesOfparent((int)row.categoryId, idsListitem);
                row.itemsCount= allitems.Where(x => idsListitem.Contains((int)x.categoryId)).ToList().Count();
            }
            ViewBag.categoryList = categoryList;
           ViewBag.currentCategory = currentCategory;
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
        public List<int> GetsonCategories(List<CategoryModel> mainList, List<int> idsList)
        {

            foreach (CategoryModel row in mainList)
            {
                idsList.Add(row.categoryId);
                List<int> tmpid = new List<int>();
                tmpid = GetSonsbyParentId(row.categoryId, idsList);
              //  idsList.Add(tmpid);
            }

            return idsList;
        }
        public List<int> GetSonsbyParentId(int? parentId, List<int> idsList)
        {

            var categoriesList = all.Where(x => x.parentId == parentId && x.isActive == 1).ToList().OrderBy(x => x.createDate).ToList();


            foreach (CategoryModel rowch in categoriesList)
            {
             idsList.Add(rowch.categoryId);
                List<int>  tmpid = new List<int>();
                // rowch.childCategories = GetSonsbyParentId(rowch.categoryId);
                tmpid= GetSonsbyParentId(rowch.categoryId, idsList);

              //  idsList.AddRange(tmpid);
            }
            return idsList;

        }

        public List<int> GetCategoriesOfparent(int categoryId, List<int> idsList)
        {
            try
            {

                //    activeCategories = all;

                List<CategoryModel> mainList = new List<CategoryModel>();
                idsList.Add(categoryId);
                mainList = GetCategoriesroot(categoryId);
                idsList = GetsonCategories(mainList, idsList);

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
                                notes = p.categoryId == categoryId ? "last" : "0",
                            }).FirstOrDefault();


                        tempcate.categoryId = category.categoryId;

                        tempcate.name = category.name;
                        tempcate.categoryCode = category.categoryCode;
                        //tempcate.createDate = category.createDate;
                        //tempcate.createUserId = category.createUserId;
                        //tempcate.details = category.details;
                        tempcate.image = category.image;
                tempcate.notes = category.notes;
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