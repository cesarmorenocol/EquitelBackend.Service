CREATE DATABASE BackendTest;
GO

USE BackendTest;
GO

CREATE SCHEMA Post;
GO

CREATE SCHEMA Analisis;
GO

CREATE TABLE Post.Post
(PostId INT IDENTITY(1,1)
,Post NVARCHAR(MAX)
,EstadoId INT NOT NULL
,FechaCreacion DATETIME2 CONSTRAINT [DF_Post_FechaCreacion] DEFAULT GETDATE()
,FechaActualizacion DATETIME2 CONSTRAINT [DF_Post_FechaActualizacion]  DEFAULT GETDATE()
,CONSTRAINT [PK_Post.Post] PRIMARY KEY (PostId));

--' Estado = { 1-Creado; 3-Enviado 2-Enviando }

CREATE TABLE Analisis.Post
(PostId INT IDENTITY(1,1) NOT NULL
,Post NVARCHAR(MAX)
,EstadoId INT NOT NULL
,CantidadPalabras INT
,CantidadOraciones INT
,CantidadParrafos INT
,Cantidadaracteres INT
,FechaCreacion DATETIME2 CONSTRAINT [DF_Analisis.Post_FechaCreacion] DEFAULT GETDATE()
,FechaActualizacion DATETIME2 CONSTRAINT [DF_Analisis.Post_FechaActualizacion]  DEFAULT GETDATE()
,CONSTRAINT [PK_Analisis.Post] PRIMARY KEY (PostId));

--' ============================================================
--' Nombre:			Post.ObtenerPostSinEnviar
--' Descripción:	Permite obtener la información de posts creados,
--'					y sin enviar al servicio de recepción de POSTS.
--' Autor:			Cesar Augusto Moreno
--' Fecha:			2017-07-08
--' ============================================================
CREATE PROCEDURE Post.ObtenerPostSinEnviar
AS
BEGIN

	SELECT PostId
		,Post
		,EstadoId
	FROM Post.Post WITH(NOLOCK)
	WHERE EstadoId = 1; -- Solo los creados
	
END
GO

--' ============================================================
--' Nombre:			Post.AdministrarPosts
--' Descripción:	Permite administrar la información de los post
--' Autor:			Cesar Augusto Moreno
--' Fecha:			2017-07-08
--' ============================================================
CREATE PROCEDURE Post.AdministrarPosts
	@Opcion INT = NULL,
	@PostId INT = NULL,
	@EstadoId INT = NULL,
	@Descripcion NVARCHAR(MAX)
AS
BEGIN
	IF @Opcion = 1
	BEGIN
		INSERT Post.Post
			(Post,EstadoId)
		VALUES 
			(@EstadoId,@Descripcion);
	END

	IF @Opcion = 2
	BEGIN
		UPDATE P
		SET EstadoId = @EstadoId
		FROM Post.Post P
		WHERE PostId = @PostId;
	END

END
GO

--' ============================================================
--' Nombre:			Analisis.AdministrarPosts
--' Descripción:	Permite administrar la información de los post
--' Autor:			Cesar Augusto Moreno
--' Fecha:			2017-07-08
--' ============================================================
CREATE PROCEDURE Analisis.AdministrarPosts
	@Opcion INT = 4,
	@Descripcion NVARCHAR(MAX) = NULL,
	@EstadoId INT = NULL,
	@PostId INT = NULL,
	@CantidadPalabras INT = NULL,
	@CantidadOraciones INT = NULL,
	@CantidadParrafos INT = NULL,
	@Cantidadaracteres INT = NULL
AS
BEGIN
	--' Inserción:
	IF @Opcion = 1
	BEGIN
		--' Crear el post a Analizar:
		INSERT INTO Analisis.Post 
			(Post,EstadoId)
		VALUES 
			(@Descripcion, @EstadoId);

		SELECT CONVERT(INT,SCOPE_IDENTITY());
	END
	--' Actualización
	IF @Opcion = 2
	BEGIN
		UPDATE P
		SET  EstadoId = @EstadoId
			,CantidadPalabras = COALESCE(@CantidadPalabras,CantidadPalabras,0)
			,CantidadOraciones = COALESCE(@CantidadOraciones,CantidadOraciones,0)
			,CantidadParrafos = COALESCE(@CantidadParrafos,CantidadParrafos,0)
			,Cantidadaracteres = COALESCE(@Cantidadaracteres,Cantidadaracteres,0)
		FROM Analisis.Post P
		WHERE PostId = @PostId;
	END
	--' Consulta
	IF @Opcion = 4
	BEGIN
		SELECT PostId
			,Post
			,EstadoId
		FROM Analisis.Post WITH(NOLOCK)
		WHERE EstadoId = 1; -- Solo los creados
	END
END
GO