-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE spConocimiento

	@Opc int = null,	
	@Nombre varchar(50) = null,
	@Def varchar(100) = null,
	@Col varchar(25) = null,
	@Ani varchar(20) = 0,
	@Gen varchar(3) = null,
	@Cont int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @Opc = 0
		SELECT TOP (1) Objecto.Id_Obj 
		FROM (SELECT * FROM Objecto) AS Objecto 
		ORDER BY Id_Obj DESC;


	IF @Opc = 1	
		SELECT Objecto.Id_Obj, Objecto.Nombre, Objecto.Definicion, Caracteristicas.Color, Caracteristicas.Genero
		FROM (
			SELECT Color.Color, Genero.Genero, Genero.Id_Obj
			FROM Color INNER JOIN Genero 
			ON Color.Id_Obj = Genero.Id_Obj
		) AS Caracteristicas
		INNER JOIN Objecto
		ON Caracteristicas.Id_Obj = Objecto.Id_Obj
		WHERE Objecto.Nombre = @Nombre;


	IF @Opc = 2
		
		INSERT INTO [dbo].[Objecto] ([Nombre],[Definicion])	VALUES (@Nombre, @Def);

		INSERT INTO [dbo].[Color]  ([Id_Obj] ,[Color]) VALUES (@Cont, @Col);

		INSERT INTO [dbo].[Animado] ([Id_Obj], [Estado]) VALUES (@Cont, @Ani);

		INSERT INTO [dbo].[Genero] ([Id_Obj], [Genero]) VALUES (@Cont, @Gen);


END
