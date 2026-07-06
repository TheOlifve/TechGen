namespace GenericSpecifications;

public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Category { get; set; }
}

public class LoanApplication 
{ 
    public string Id { get; set; } 
    public int Score { get; set; } 
    public int Income { get; set; } 
}

public class Candidate 
{ 
    public string Id { get; set; } 
    public int Experience { get; set; } 
    public string[] Skills { get; set; } 
    public int SalaryAsk { get; set; } 
    public string Location { get; set; } 
}

public class ShipmentOrder 
{ 
    public string Id { get; set; } 
    public double Weight { get; set; } 
    public bool Fragile { get; set; } 
    public bool Express { get; set; } 
    public string Zone { get; set; } 
}

public static class Demo
{
    public static void RunProductDemo()
    {
        var products = new List<Product>
        {
            new Product { Name = "Keyboard", Price = 80, Stock = 3, Category = "Electronics" },
            new Product { Name = "Monitor", Price = 95, Stock = 8, Category = "Electronics" },
            new Product { Name = "Mouse", Price = 25, Stock = 0, Category = "Electronics" },
            new Product { Name = "Laptop", Price = 1200, Stock = 5, Category = "Electronics" }
        };

        var electronics = new PredicateSpecification<Product>(p => p.Category == "Electronics");
        var inStock = new PredicateSpecification<Product>(p => p.Stock > 0);
        var affordable = new PredicateSpecification<Product>(p => p.Price <= 100m);
        var outOfStock = new PredicateSpecification<Product>(p => p.Stock == 0);
        
        var promoEligible = electronics.And(inStock).And(affordable);
        var restock = electronics.And(outOfStock);
        var premium = electronics.And(new PredicateSpecification<Product>(p => p.Price >= 500m));

        Console.WriteLine("Promo eligible:");
        foreach (var p in products.Where(promoEligible))
            Console.WriteLine($"{p.Name} (${p.Price}, stock={p.Stock})");

        Console.WriteLine("Restock candidates:");
        foreach (var p in products.Where(restock))
            Console.WriteLine($"{p.Name} (stock={p.Stock})");

        Console.WriteLine("Premium electronics (first match):");
        var firstPremium = products.FirstOrDefault(premium);
        if (firstPremium != null) Console.WriteLine($"{firstPremium.Name} (${firstPremium.Price})");

        Console.WriteLine($"Affordable electronics count: {products.Count(affordable)}");
    }
    
    public static void RunLoanDemo()
    {
        var applications = new List<LoanApplication>
        {
            new LoanApplication { Id = "01", Score = 720, Income = 4200 },
            new LoanApplication { Id = "02", Score = 680, Income = 2800 },
            new LoanApplication { Id = "03", Score = 590, Income = 3100 },
            new LoanApplication { Id = "04", Score = 710, Income = 1900 },
            new LoanApplication { Id = "05", Score = 640, Income = 3500 },
            new LoanApplication { Id = "06", Score = 670, Income = 3400 }
        };

        var goodCredit = new PredicateSpecification<LoanApplication>(a => a.Score >= 700);
        var goodIncome = new PredicateSpecification<LoanApplication>(a => a.Income >= 4000);
        var okCredit = new PredicateSpecification<LoanApplication>(a => a.Score >= 650);
        var okIncome = new PredicateSpecification<LoanApplication>(a => a.Income >= 3000);

        var approved = goodCredit.And(goodIncome);
        var manualReview = okCredit.And(okIncome).And(approved.Not());

        foreach (var app in applications)
        {
            string status = "REJECTED";
            if (approved.IsSatisfiedBy(app)) status = "APPROVED";
            else if (manualReview.IsSatisfiedBy(app)) status = "MANUAL REVIEW";

            Console.WriteLine($"{app.Id}: score={app.Score}, income={app.Income} -> {status}");
        }
    }
    
    public static void RunOrderDemo()
    {
        var orders = new List<ShipmentOrder>
        {
            new ShipmentOrder { Id = "O-100", Weight = 1.2, Fragile = false, Express = false, Zone = "Local" },
            new ShipmentOrder { Id = "O-101", Weight = 18.5, Fragile = false, Express = true, Zone = "Local" },
            new ShipmentOrder { Id = "O-102", Weight = 0.8, Fragile = true, Express = false, Zone = "International" },
            new ShipmentOrder { Id = "O-103", Weight = 25, Fragile = true, Express = true, Zone = "International" },
            new ShipmentOrder { Id = "O-104", Weight = 4.5, Fragile = false, Express = false, Zone = "Regional" }
        };

        var heavy = new PredicateSpecification<ShipmentOrder>(o => o.Weight > 15);
        var fragile = new PredicateSpecification<ShipmentOrder>(o => o.Fragile);
        
        var freight = heavy.Or(fragile);

        foreach (var o in orders)
        {
            string route = "STANDARD";
            if (freight.IsSatisfiedBy(o)) route = "FREIGHT";

            Console.WriteLine($"{o.Id}: {o.Weight}kg, fragile={o.Fragile}, express={o.Express}, zone={o.Zone} -> {route}");
        }
    }
    
    public static void RunJobDemo()
    {
        var candidates = new List<Candidate>
        {
            new Candidate { Id = "C-01", Experience = 6, Skills = new[]{"C#", ".NET", "SQL"}, SalaryAsk = 4500, Location = "Local" },
            new Candidate { Id = "C-04", Experience = 8, Skills = new[]{"C#", ".NET", "Docker"}, SalaryAsk = 3800, Location = "Local" },
            new Candidate { Id = "C-03", Experience = 5, Skills = new[]{"C#", "Azure"}, SalaryAsk = 4000, Location = "Remote" }
        };

        var expRange = new PredicateSpecification<Candidate>(c => c.Experience >= 3 && c.Experience <= 20);
        var salaryRange = new PredicateSpecification<Candidate>(c => c.SalaryAsk >= 3000 && c.SalaryAsk <= 5000);
        
        var isDotNetSkill = new PredicateSpecification<string>(s => s == "C#" || s == ".NET");
        var hasDotNet = new HasAnySpecification<Candidate, string>(c => c.Skills, isDotNetSkill);

        var shortlist = expRange.And(salaryRange).And(hasDotNet);
        var remote = new PredicateSpecification<Candidate>(c => c.Location == "Remote");
        var backup = hasDotNet.And(remote).And(shortlist.Not());

        Console.WriteLine("Shortlist:");
        foreach (var c in candidates.Where(shortlist))
            Console.WriteLine($"{c.Id}: {c.Experience}y, skills=[{string.Join(", ", c.Skills)}], ask={c.SalaryAsk}");

        Console.WriteLine("Backup pool:");
        foreach (var c in candidates.Where(backup))
            Console.WriteLine($"{c.Id}: {c.Location}, skills=[{string.Join(", ", c.Skills)}]");
    }
}
