CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "AspNetRoles" (
    "Id" nvarchar(450) NOT NULL CONSTRAINT "PK_AspNetRoles" PRIMARY KEY,
    "Name" nvarchar(256) NULL,
    "NormalizedName" nvarchar(256) NULL,
    "ConcurrencyStamp" TEXT NULL
);

CREATE TABLE "AspNetUsers" (
    "Id" nvarchar(450) NOT NULL CONSTRAINT "PK_AspNetUsers" PRIMARY KEY,
    "UserName" nvarchar(256) NULL,
    "NormalizedUserName" nvarchar(256) NULL,
    "Email" nvarchar(256) NULL,
    "NormalizedEmail" nvarchar(256) NULL,
    "EmailConfirmed" bit NOT NULL,
    "PasswordHash" TEXT NULL,
    "SecurityStamp" TEXT NULL,
    "ConcurrencyStamp" TEXT NULL,
    "PhoneNumber" TEXT NULL,
    "PhoneNumberConfirmed" bit NOT NULL,
    "TwoFactorEnabled" bit NOT NULL,
    "LockoutEnd" datetimeoffset NULL,
    "LockoutEnabled" bit NOT NULL,
    "AccessFailedCount" int NOT NULL
);

CREATE TABLE "AspNetRoleClaims" (
    "Id" int NOT NULL CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY,
    "RoleId" nvarchar(450) NOT NULL,
    "ClaimType" TEXT NULL,
    "ClaimValue" TEXT NULL,
    CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserClaims" (
    "Id" int NOT NULL CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY,
    "UserId" nvarchar(450) NOT NULL,
    "ClaimType" TEXT NULL,
    "ClaimValue" TEXT NULL,
    CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserLogins" (
    "LoginProvider" nvarchar(128) NOT NULL,
    "ProviderKey" nvarchar(128) NOT NULL,
    "ProviderDisplayName" TEXT NULL,
    "UserId" nvarchar(450) NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserRoles" (
    "UserId" nvarchar(450) NOT NULL,
    "RoleId" nvarchar(450) NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserTokens" (
    "UserId" nvarchar(450) NOT NULL,
    "LoginProvider" nvarchar(128) NOT NULL,
    "Name" nvarchar(128) NOT NULL,
    "Value" TEXT NULL,
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
    CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON "AspNetRoleClaims" ("RoleId");

CREATE UNIQUE INDEX "RoleNameIndex" ON "AspNetRoles" ("NormalizedName") WHERE [NormalizedName] IS NOT NULL;

CREATE INDEX "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" ("UserId");

CREATE INDEX "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" ("UserId");

CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" ("RoleId");

CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail");

CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName") WHERE [NormalizedUserName] IS NOT NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('00000000000000_CreateIdentitySchema', '6.0.10');

COMMIT;

BEGIN TRANSACTION;

ALTER TABLE "Hunt" RENAME TO "Hunts";

CREATE TABLE "AccessCode" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_AccessCode" PRIMARY KEY AUTOINCREMENT,
    "HuntId" INTEGER NULL,
    CONSTRAINT "FK_AccessCode_Hunts_HuntId" FOREIGN KEY ("HuntId") REFERENCES "Hunts" ("Id")
);

CREATE TABLE "Location" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Location" PRIMARY KEY AUTOINCREMENT,
    "HuntId" INTEGER NOT NULL,
    "Place" TEXT NOT NULL,
    "Lat" REAL NULL,
    "Lon" REAL NULL,
    "Task" TEXT NOT NULL,
    "AccessCode" TEXT NULL,
    "QRCode" TEXT NULL,
    "Answer" TEXT NULL
);

CREATE INDEX "IX_AccessCode_HuntId" ON "AccessCode" ("HuntId");

CREATE TABLE "ef_temp_AspNetUsers" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_AspNetUsers" PRIMARY KEY,
    "AccessFailedCount" INTEGER NOT NULL,
    "ConcurrencyStamp" TEXT NULL,
    "Email" TEXT NULL,
    "EmailConfirmed" INTEGER NOT NULL,
    "FirstName" TEXT NOT NULL,
    "HuntId" INTEGER NULL,
    "LastName" TEXT NOT NULL,
    "LockoutEnabled" INTEGER NOT NULL,
    "LockoutEnd" TEXT NULL,
    "NormalizedEmail" TEXT NULL,
    "NormalizedUserName" TEXT NULL,
    "PasswordHash" TEXT NULL,
    "PhoneNumber" TEXT NULL,
    "PhoneNumberConfirmed" INTEGER NOT NULL,
    "SecurityStamp" TEXT NULL,
    "TwoFactorEnabled" INTEGER NOT NULL,
    "UserName" TEXT NULL,
    CONSTRAINT "FK_AspNetUsers_Hunts_HuntId" FOREIGN KEY ("HuntId") REFERENCES "Hunts" ("Id")
);

INSERT INTO "ef_temp_AspNetUsers" ("Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HuntId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName")
SELECT "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", IFNULL("FirstName", ''), "HuntId", IFNULL("LastName", ''), "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName"
FROM "AspNetUsers";

CREATE TABLE "ef_temp_Hunts" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Hunts" PRIMARY KEY AUTOINCREMENT
);

INSERT INTO "ef_temp_Hunts" ("Id")
SELECT "Id"
FROM "Hunts";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "AspNetUsers";

ALTER TABLE "ef_temp_AspNetUsers" RENAME TO "AspNetUsers";

DROP TABLE "Hunts";

ALTER TABLE "ef_temp_Hunts" RENAME TO "Hunts";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail");

CREATE INDEX "IX_AspNetUsers_HuntId" ON "AspNetUsers" ("HuntId");

CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221107190947_mig01', '6.0.10');

COMMIT;

BEGIN TRANSACTION;

ALTER TABLE "AspNetUsers" ADD "AccessCodeId" INTEGER NULL;

CREATE INDEX "IX_AspNetUsers_AccessCodeId" ON "AspNetUsers" ("AccessCodeId");

CREATE TABLE "ef_temp_AspNetUsers" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_AspNetUsers" PRIMARY KEY,
    "AccessCodeId" INTEGER NULL,
    "AccessFailedCount" INTEGER NOT NULL,
    "ConcurrencyStamp" TEXT NULL,
    "Email" TEXT NULL,
    "EmailConfirmed" INTEGER NOT NULL,
    "FirstName" TEXT NOT NULL,
    "HuntId" INTEGER NULL,
    "LastName" TEXT NOT NULL,
    "LockoutEnabled" INTEGER NOT NULL,
    "LockoutEnd" TEXT NULL,
    "NormalizedEmail" TEXT NULL,
    "NormalizedUserName" TEXT NULL,
    "PasswordHash" TEXT NULL,
    "PhoneNumber" TEXT NULL,
    "PhoneNumberConfirmed" INTEGER NOT NULL,
    "SecurityStamp" TEXT NULL,
    "TwoFactorEnabled" INTEGER NOT NULL,
    "UserName" TEXT NULL,
    CONSTRAINT "FK_AspNetUsers_AccessCode_AccessCodeId" FOREIGN KEY ("AccessCodeId") REFERENCES "AccessCode" ("Id"),
    CONSTRAINT "FK_AspNetUsers_Hunts_HuntId" FOREIGN KEY ("HuntId") REFERENCES "Hunts" ("Id")
);

INSERT INTO "ef_temp_AspNetUsers" ("Id", "AccessCodeId", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HuntId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName")
SELECT "Id", "AccessCodeId", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HuntId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName"
FROM "AspNetUsers";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "AspNetUsers";

ALTER TABLE "ef_temp_AspNetUsers" RENAME TO "AspNetUsers";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail");

CREATE INDEX "IX_AspNetUsers_AccessCodeId" ON "AspNetUsers" ("AccessCodeId");

CREATE INDEX "IX_AspNetUsers_HuntId" ON "AspNetUsers" ("HuntId");

CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221107203543_mig02', '6.0.10');

COMMIT;

BEGIN TRANSACTION;

ALTER TABLE "Hunts" ADD "HuntName" TEXT NULL;

ALTER TABLE "AccessCode" ADD "Code" TEXT NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221107211608_mig03', '6.0.10');

COMMIT;

BEGIN TRANSACTION;

ALTER TABLE "AccessCode" RENAME TO "AccessCodes";

DROP INDEX "IX_AccessCode_HuntId";

CREATE INDEX "IX_AccessCodes_HuntId" ON "AccessCodes" ("HuntId");

CREATE TABLE "ef_temp_AccessCodes" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_AccessCodes" PRIMARY KEY AUTOINCREMENT,
    "Code" TEXT NULL,
    "HuntId" INTEGER NOT NULL,
    CONSTRAINT "FK_AccessCodes_Hunts_HuntId" FOREIGN KEY ("HuntId") REFERENCES "Hunts" ("Id") ON DELETE CASCADE
);

INSERT INTO "ef_temp_AccessCodes" ("Id", "Code", "HuntId")
SELECT "Id", "Code", IFNULL("HuntId", 0)
FROM "AccessCodes";

CREATE TABLE "ef_temp_AspNetUsers" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_AspNetUsers" PRIMARY KEY,
    "AccessCodeId" INTEGER NULL,
    "AccessFailedCount" INTEGER NOT NULL,
    "ConcurrencyStamp" TEXT NULL,
    "Email" TEXT NULL,
    "EmailConfirmed" INTEGER NOT NULL,
    "FirstName" TEXT NOT NULL,
    "HuntId" INTEGER NULL,
    "LastName" TEXT NOT NULL,
    "LockoutEnabled" INTEGER NOT NULL,
    "LockoutEnd" TEXT NULL,
    "NormalizedEmail" TEXT NULL,
    "NormalizedUserName" TEXT NULL,
    "PasswordHash" TEXT NULL,
    "PhoneNumber" TEXT NULL,
    "PhoneNumberConfirmed" INTEGER NOT NULL,
    "SecurityStamp" TEXT NULL,
    "TwoFactorEnabled" INTEGER NOT NULL,
    "UserName" TEXT NULL,
    CONSTRAINT "FK_AspNetUsers_AccessCodes_AccessCodeId" FOREIGN KEY ("AccessCodeId") REFERENCES "AccessCodes" ("Id"),
    CONSTRAINT "FK_AspNetUsers_Hunts_HuntId" FOREIGN KEY ("HuntId") REFERENCES "Hunts" ("Id")
);

INSERT INTO "ef_temp_AspNetUsers" ("Id", "AccessCodeId", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HuntId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName")
SELECT "Id", "AccessCodeId", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HuntId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName"
FROM "AspNetUsers";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "AccessCodes";

ALTER TABLE "ef_temp_AccessCodes" RENAME TO "AccessCodes";

DROP TABLE "AspNetUsers";

ALTER TABLE "ef_temp_AspNetUsers" RENAME TO "AspNetUsers";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

CREATE INDEX "IX_AccessCodes_HuntId" ON "AccessCodes" ("HuntId");

CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail");

CREATE INDEX "IX_AspNetUsers_AccessCodeId" ON "AspNetUsers" ("AccessCodeId");

CREATE INDEX "IX_AspNetUsers_HuntId" ON "AspNetUsers" ("HuntId");

CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221107233038_mig04', '6.0.10');

COMMIT;

BEGIN TRANSACTION;

CREATE TABLE "ef_temp_AspNetUsers" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_AspNetUsers" PRIMARY KEY,
    "AccessCodeId" INTEGER NULL,
    "AccessFailedCount" INTEGER NOT NULL,
    "ConcurrencyStamp" TEXT NULL,
    "Email" TEXT NULL,
    "EmailConfirmed" INTEGER NOT NULL,
    "FirstName" TEXT NOT NULL,
    "HuntId" INTEGER NULL,
    "LastName" TEXT NOT NULL,
    "LockoutEnabled" INTEGER NOT NULL,
    "LockoutEnd" TEXT NULL,
    "NormalizedEmail" TEXT NULL,
    "NormalizedUserName" TEXT NULL,
    "PasswordHash" TEXT NULL,
    "PhoneNumber" TEXT NULL,
    "PhoneNumberConfirmed" INTEGER NOT NULL,
    "SecurityStamp" TEXT NULL,
    "TwoFactorEnabled" INTEGER NOT NULL,
    "UserName" TEXT NULL,
    CONSTRAINT "FK_AspNetUsers_AccessCodes_AccessCodeId" FOREIGN KEY ("AccessCodeId") REFERENCES "AccessCodes" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_AspNetUsers_Hunts_HuntId" FOREIGN KEY ("HuntId") REFERENCES "Hunts" ("Id") ON DELETE CASCADE
);

INSERT INTO "ef_temp_AspNetUsers" ("Id", "AccessCodeId", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HuntId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName")
SELECT "Id", "AccessCodeId", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HuntId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName"
FROM "AspNetUsers";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "AspNetUsers";

ALTER TABLE "ef_temp_AspNetUsers" RENAME TO "AspNetUsers";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail");

CREATE INDEX "IX_AspNetUsers_AccessCodeId" ON "AspNetUsers" ("AccessCodeId");

CREATE INDEX "IX_AspNetUsers_HuntId" ON "AspNetUsers" ("HuntId");

CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221108012703_mig05', '6.0.10');

COMMIT;

