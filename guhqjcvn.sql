﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'bookings') THEN
        CREATE SCHEMA bookings;
    END IF;
END $EF$;

CREATE TABLE bookings."AspNetRoles" (
    "Id" text NOT NULL,
    "Name" character varying(256) NULL,
    "NormalizedName" character varying(256) NULL,
    "ConcurrencyStamp" text NULL,
    CONSTRAINT "PK_AspNetRoles" PRIMARY KEY ("Id")
);

CREATE TABLE bookings.troupers (
    "Id" char(25) NOT NULL,
    passenger_name text NULL,
    "Country" varchar(35) NULL,
    "UserName" character varying(256) NULL,
    "NormalizedUserName" character varying(256) NULL,
    "Email" character varying(256) NULL,
    "NormalizedEmail" character varying(256) NULL,
    "EmailConfirmed" boolean NOT NULL,
    "PasswordHash" text NULL,
    "SecurityStamp" text NULL,
    "ConcurrencyStamp" text NULL,
    "PhoneNumber" varchar(15) NULL,
    "PhoneNumberConfirmed" boolean NOT NULL,
    "TwoFactorEnabled" boolean NOT NULL,
    "LockoutEnd" timestamp with time zone NULL,
    "LockoutEnabled" boolean NOT NULL,
    "AccessFailedCount" integer NOT NULL,
    CONSTRAINT "PK_troupers" PRIMARY KEY ("Id")
);

CREATE TABLE bookings."AspNetRoleClaims" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "RoleId" text NOT NULL,
    "ClaimType" text NULL,
    "ClaimValue" text NULL,
    CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES bookings."AspNetRoles" ("Id") ON DELETE CASCADE
);

CREATE TABLE bookings."AspNetUserClaims" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "UserId" char(25) NOT NULL,
    "ClaimType" text NULL,
    "ClaimValue" text NULL,
    CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetUserClaims_troupers_UserId" FOREIGN KEY ("UserId") REFERENCES bookings.troupers ("Id") ON DELETE CASCADE
);

CREATE TABLE bookings."AspNetUserLogins" (
    "LoginProvider" text NOT NULL,
    "ProviderKey" text NOT NULL,
    "ProviderDisplayName" text NULL,
    "UserId" char(25) NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_AspNetUserLogins_troupers_UserId" FOREIGN KEY ("UserId") REFERENCES bookings.troupers ("Id") ON DELETE CASCADE
);

CREATE TABLE bookings."AspNetUserRoles" (
    "UserId" char(25) NOT NULL,
    "RoleId" text NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES bookings."AspNetRoles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_AspNetUserRoles_troupers_UserId" FOREIGN KEY ("UserId") REFERENCES bookings.troupers ("Id") ON DELETE CASCADE
);

CREATE TABLE bookings."AspNetUserTokens" (
    "UserId" char(25) NOT NULL,
    "LoginProvider" text NOT NULL,
    "Name" text NOT NULL,
    "Value" text NULL,
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
    CONSTRAINT "FK_AspNetUserTokens_troupers_UserId" FOREIGN KEY ("UserId") REFERENCES bookings.troupers ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON bookings."AspNetRoleClaims" ("RoleId");

CREATE UNIQUE INDEX "RoleNameIndex" ON bookings."AspNetRoles" ("NormalizedName");

CREATE INDEX "IX_AspNetUserClaims_UserId" ON bookings."AspNetUserClaims" ("UserId");

CREATE INDEX "IX_AspNetUserLogins_UserId" ON bookings."AspNetUserLogins" ("UserId");

CREATE INDEX "IX_AspNetUserRoles_RoleId" ON bookings."AspNetUserRoles" ("RoleId");

CREATE INDEX "EmailIndex" ON bookings.troupers ("NormalizedEmail");

CREATE UNIQUE INDEX "UserNameIndex" ON bookings.troupers ("NormalizedUserName");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230904045943_CreatingtheTrouper', '7.0.10');

COMMIT;

START TRANSACTION;

UPDATE bookings.troupers SET passenger_name = '' WHERE passenger_name IS NULL;
ALTER TABLE bookings.troupers ALTER COLUMN passenger_name SET NOT NULL;
ALTER TABLE bookings.troupers ALTER COLUMN passenger_name SET DEFAULT '';

UPDATE bookings.troupers SET "Country" = '' WHERE "Country" IS NULL;
ALTER TABLE bookings.troupers ALTER COLUMN "Country" SET NOT NULL;
ALTER TABLE bookings.troupers ALTER COLUMN "Country" SET DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230904050128_CreatingtheTrouper2', '7.0.10');

COMMIT;

