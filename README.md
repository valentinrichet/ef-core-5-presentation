# Entity Framework Core 5 - Présentation

1. Créer un secret / changer les appsettings pour configurer la base de données [ConnectionStrings:EfCorePresentation]

*Exemple :*

```
{
    "ConnectionStrings:EfCorePresentation": "Server=localhost\\SQLEXPRESS;Database=efcore-presentation;Trusted_Connection=True;"
}
```

2. Installer l'utilitaire dotnet-ef 

```
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef
```

3. Appliquer les changements à la base de données

```
dotnet ef database update 
```

4. **ENJOY 😁**
