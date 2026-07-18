CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Projects" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Projects" PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "Description" TEXT NULL,
    "Status" TEXT NOT NULL,
    "CreatedAt" TEXT NOT NULL,
    "NormalizedName" TEXT NOT NULL
);

CREATE TABLE "TaskItems" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_TaskItems" PRIMARY KEY,
    "Title" TEXT NOT NULL,
    "Description" TEXT NULL,
    "Status" TEXT NOT NULL,
    "Priority" TEXT NOT NULL,
    "CreatedAt" TEXT NOT NULL,
    "CompletedAt" TEXT NULL,
    "ProjectId" TEXT NOT NULL,
    "NormalizedTitle" TEXT NOT NULL,
    CONSTRAINT "FK_TaskItems_Projects_ProjectId" FOREIGN KEY ("ProjectId") REFERENCES "Projects" ("Id") ON DELETE RESTRICT
);

CREATE UNIQUE INDEX "IX_Projects_NormalizedName" ON "Projects" ("NormalizedName");

CREATE UNIQUE INDEX "IX_TaskItems_ProjectId_NormalizedTitle" ON "TaskItems" ("ProjectId", "NormalizedTitle");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260718184406_InitialCreate', '8.0.0');

COMMIT;

