CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251217131909_InitialCreate') THEN
    CREATE TABLE "Libraries" (
        "Id" uuid NOT NULL,
        "Name" character varying(200) NOT NULL,
        "Address" character varying(300) NOT NULL,
        CONSTRAINT "PK_Libraries" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251217131909_InitialCreate') THEN
    CREATE TABLE "Members" (
        "Id" uuid NOT NULL,
        "Name" character varying(100) NOT NULL,
        "Email" character varying(200) NOT NULL,
        "RegisteredAt" timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Members" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251217131909_InitialCreate') THEN
    CREATE TABLE "Books" (
        "Id" uuid NOT NULL,
        "Isbn" character varying(13) NOT NULL,
        "Title" character varying(200) NOT NULL,
        "Author" character varying(100) NOT NULL,
        "TotalCopies" integer NOT NULL,
        "AvailableCopies" integer NOT NULL,
        "LibraryId" uuid NOT NULL,
        CONSTRAINT "PK_Books" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Books_Libraries_LibraryId" FOREIGN KEY ("LibraryId") REFERENCES "Libraries" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251217131909_InitialCreate') THEN
    CREATE TABLE "Loans" (
        "Id" uuid NOT NULL,
        "BookId" uuid NOT NULL,
        "MemberId" uuid NOT NULL,
        "BorrowedAt" timestamp with time zone NOT NULL,
        "ReturnedAt" timestamp with time zone,
        "DueDate" timestamp with time zone NOT NULL,
        CONSTRAINT "PK_Loans" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Loans_Books_BookId" FOREIGN KEY ("BookId") REFERENCES "Books" ("Id") ON DELETE RESTRICT,
        CONSTRAINT "FK_Loans_Members_MemberId" FOREIGN KEY ("MemberId") REFERENCES "Members" ("Id") ON DELETE RESTRICT
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251217131909_InitialCreate') THEN
    CREATE INDEX "IX_Books_LibraryId" ON "Books" ("LibraryId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251217131909_InitialCreate') THEN
    CREATE INDEX "IX_Loans_BookId" ON "Loans" ("BookId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251217131909_InitialCreate') THEN
    CREATE INDEX "IX_Loans_MemberId" ON "Loans" ("MemberId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251217131909_InitialCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20251217131909_InitialCreate', '9.0.1');
    END IF;
END $EF$;
COMMIT;

