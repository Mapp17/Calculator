using Microsoft.AspNetCore.Identity;

public static class IdentitySeeder
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if(!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }
        var admin = await userManager.FindByNameAsync("Syke");

        if(admin == null)
        {
            admin = new ApplicationUser
            {
                UserName = "Skye",
                Email = "skye@Calculator.com"
            };

            await userManager.CreateAsync(admin, "Skye123!");
            await userManager.AddToRoleAsync(admin, "Admin");
        } 
    }
}