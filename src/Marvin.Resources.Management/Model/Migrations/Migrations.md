﻿## Migration calls

# Add Migration

Add-Migration -Name InitialCreate -ProjectName Marvin.Resources.Management -ConnectionString "Username=postgres;Password=postgres;Host=localhost;Port=5432;Persist Security Info=True;Database=resources" -ConnectionProviderName Npgsql -Verbose

# Update Migration

Update-Database -TargetMigration InitialCreate -ProjectName Marvin.Resources.Management -ConnectionString "Username=postgres;Password=postgres;Host=localhost;Port=5432;Persist Security Info=True;Database=resources" -ConnectionProviderName Npgsql -Verbose
