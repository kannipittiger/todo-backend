using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DataContext;
using Models.Request;
using Models.Response;
using Services.ProductInterface;

namespace Services.Product
{
    public class ProductService : IProduct
    {
        private readonly DataContext _data;
        public ProductService(DataContext data)
        {
            _data = data;
        }

        public ProductRes SaveProduct(ProductReq req)
        {
            using var transaction = _data.Database.BeginTransaction();

            try
            {
                string[] allowedCategories = { "อาหาร", "เครื่องดื่ม", "ของใช้", "เสื้อผ้า" };
                // 1️⃣ Name: ต้องไม่ว่าง
                if (string.IsNullOrWhiteSpace(req.ProductName))
                {
                    return new ProductRes
                    {
                        Success = false,
                        Message = "Name ต้องไม่ว่าง"
                    };
                }

                // 2️⃣ SKU: ต้องไม่ว่าง, ยาว >= 3, ต้องไม่ซ้ำ
                if (string.IsNullOrWhiteSpace(req.Sku))
                {
                    return new ProductRes
                    {
                        Success = false,
                        Message = "SKU ต้องไม่ว่าง"
                    };
                }

                if (req.Sku.Length < 3)
                {
                    return new ProductRes
                    {
                        Success = false,
                        Message = "SKU ต้องยาวอย่างน้อย 3 ตัวอักษร"
                    };
                }

                // ตรวจสอบ SKU ซ้ำ
                bool skuExists = _data.ProductManagements.Any(p => p.Sku == req.Sku);
                if (skuExists)
                {
                    return new ProductRes
                    {
                        Success = false,
                        Message = "SKU นี้มีอยู่แล้ว"
                    };
                }

                // 3️⃣ Price: ต้องมากกว่า 0
                if (!req.Price.HasValue || req.Price <= 0)
                {
                    return new ProductRes
                    {
                        Success = false,
                        Message = "Price ต้องมากกว่า 0"
                    };
                }

                // 4️⃣ Stock: ต้องไม่ติดลบ
                if (!req.Stock.HasValue || req.Stock < 0)
                {
                    return new ProductRes
                    {
                        Success = false,
                        Message = "Stock ต้อง >= 0"
                    };
                }

                // 5️⃣ Category: ต้องเป็น 1 ใน 4 หมวด
                if (string.IsNullOrWhiteSpace(req.Category) || !allowedCategories.Contains(req.Category))
                {
                    return new ProductRes
                    {
                        Success = false,
                        Message = $"Category ต้องเป็นหนึ่งใน: {string.Join(", ", allowedCategories)}"
                    };
                }

                int id = 1; // ค่าเริ่มต้นถ้าไม่มีข้อมูล

                var latestProduct = _data.ProductManagements
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefault();

                if (latestProduct != null)
                {
                    id = latestProduct.Id;
                }

                // ถ้า validation ผ่านทั้งหมด → สร้าง entity
                var product = new ProductManagement
                {
                    Sku = req.Sku,
                    ProductName = req.ProductName,
                    Price = req.Price.Value,
                    Stock = req.Stock.Value,
                    Category = req.Category
                };

                _data.ProductManagements.Add(product);
                _data.SaveChanges();

                transaction.Commit();

                return new ProductRes
                {
                    Id = id,
                    Sku = req.Sku,
                    ProductName = req.ProductName,
                    Price = req.Price.Value,
                    Stock = req.Stock.Value,
                    Category = req.Category,
                    Success = true,
                    Message = "บันทึกสำเร็จ"
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                return new ProductRes
                {
                    Success = false,
                    Message = $"Error: {ex.Message}",
                };
            }
        }

        public List<ProductManagement> GetProduct (string category)
        {
            var query = from x in _data.ProductManagements
            select new ProductManagement
            {
                Id = x.Id,
                ProductName = x.ProductName,
                Price = x.Price,
                Stock = x.Stock,
                Sku = x.Sku,
                Category = x. Category
            };

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(x => x.Category == category);
            }

            return query.ToList();
        }
    }
}
