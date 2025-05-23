## Принципи програмування

### 1. **DRY**
- Метод `Normalize()` у [`Money.cs`](ConsoleApp1/Models/Money.cs) використовується для перетворення центів у долари.

### 2. **KISS**
- Клас `Product` містить лише необхідні методи:  
  [Product.cs](Models/Product.cs).

### 3. **SOLID**
- **SRP (Single Responsibility Principle)**: 
  - `Warehouse` відповідає лише за управління запасами: [Warehouse.cs](ConsoleApp1/Models/Warehouse.cs).
- **LSP (Liskov Substitution Principle)**: 
  - `Reporting` працює з будь-яким об’єктом, що імітує `Warehouse`.

### 4. **Composition Over Inheritance**
- `Product` використовує об’єкт `Money` замість наслідування:  
  [Product.cs](ConsoleApp1/Models/Product.cs).

---

## Структура проекту

```
ConsoleApp1/
├── Models/
│   ├── Money.cs           # Робота з валютою (грошові операції)
│   ├── Product.cs         # Опис товару (назва, ціна, одиниця виміру)
│   ├── WarehouseItem.cs   # Елемент складу (товар, кількість, дата)
│   └── Warehouse.cs       # Управління запасами (додавання/видалення)
├── Services/
│   └── Reporting.cs       # Генерація звітів (накладні, інвентаризація)
└── Program.cs             # Точка входу (тестування функціоналу)
```

## Посилання на файли
  - [Money.cs](ConsoleApp1/Models/Money.cs)
  - [Product.cs](ConsoleApp1/Models/Product.cs)
  - [Warehouse.cs](ConsoleApp1/Models/Warehouse.cs)
  - [WarehouseItem.cs](ConsoleApp1/Models/WarehouseItem.cs)
  - [Reporting.cs](ConsoleApp1/Services/Reporting.cs)
  - [Program.cs](ConsoleApp1/Program.cs)