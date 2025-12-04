# Sistema de Gestión de Órdenes de Inspección (CU37)

> **Trabajo Práctico / Caso de Uso 37**
> Solución de escritorio para la administración del ciclo de vida de órdenes de inspección, implementada con arquitectura en capas y patrones de diseño.

## Descripción

Este proyecto implementa el caso de uso **CU37 – Gestionar Órdenes de Inspección**. El sistema permite a los usuarios crear, consultar, filtrar, actualizar y cerrar órdenes asignadas a inspectores. 

El núcleo del sistema se basa en un manejo robusto de estados para garantizar la integridad del flujo de trabajo, persistencia en base de datos relacional y una interfaz gráfica intuitiva.

---

## Objetivos del Proyecto

* **Administración Integral:** Gestión completa del ciclo de vida de las órdenes de inspección.
* **Control de Flujo:** Manejo estricto de las transiciones entre estados mediante el **Patrón State**.
* **Auditoría:** Registro automático de fechas y horarios críticos (inicio, fin y cierre).
* **Gestión de Entidades:** Administración de Inspectores, Estaciones y Estados persistidos.
* **Búsqueda Avanzada:** Consultas filtradas por múltiples criterios.

---

## Tecnologías Utilizadas

* **Lenguaje:** C# (.NET)
* **Interfaz:** Windows Forms (WinForms)
* **Base de Datos:** SQL Server
* **Patrones de Diseño:** State Pattern, Factory Pattern.
* **Persistencia:** DAO (Data Access Object).

---

## Arquitectura y Diseño

El sistema utiliza una **Arquitectura en Capas** para asegurar la separación de responsabilidades y la mantenibilidad.

### Entidades del Dominio
* `OrdenDeInspeccion`: La entidad principal.
* `Inspector` y `Estacion`: Entidades de soporte.
* `Estado`: Representación del estado persistido en la BD.
* `IEstado`: Interfaz base para el patrón de comportamiento.

### Manejo de Estados (Patrón State)
Se implementó el patrón State para encapsular la lógica de negocio de cada etapa:

1.  **EstadoFactory:** Se encarga de convertir el `id_estado` o nombre de la BD en un objeto de estado funcional del dominio.
2.  **Estados Concretos:**
    * **Realizada:** Estado activo que permite registrar la fecha de fin y avanzar al cierre.
    * **Cerrada:** Estado final. Se registra fecha de cierre y observaciones. No permite más modificaciones (inmutabilidad).

**Flujo de Transición:**
`Creación` ➔ `Inicial (según BD)` ➔ `Realizada` ➔ `Cerrada`

### Persistencia (Capa DAO)
La capa de acceso a datos se encarga de:
* Mapeo Objeto-Relacional (ORM manual).
* Operaciones CRUD (Insertar, Modificar, Consultar).
* Ejecución de cambios de estado transaccionales.

---

## Base de Datos

El sistema requiere las siguientes tablas principales en SQL Server:

| Tabla | Descripción |
| :--- | :--- |
| **`ORDEN_INSPECCION`** | Almacena los datos de la orden y sus fechas. |
| **`ESTADO`** | Catálogo de estados posibles. |
| **`INSPECTOR`** | Datos de los inspectores asignables. |
| **`ESTACION`** | Datos de las estaciones de trabajo. |

> **IMPORTANTE:** Los valores de la columna `nombre_estado` en la base de datos deben coincidir **exactamente** con los utilizados en el código C# (ej: `"REALIZADA"`, `"CERRADA"`) para que el `EstadoFactory` funcione correctamente.

---

## Estructura del Proyecto
ImplementacionCU37/
 ├──  Entidades/       # Clases del dominio (Orden, Inspector, etc.)
 ├──  Estados/         # Implementación del Patrón State (IEstado, Realizada...)
 ├──  DAO/             # Capa de Acceso a Datos (Consultas SQL)
 ├──  Servicios/       # Lógica de negocio y orquestación
 ├──  UI/              # Formularios e Interfaz Gráfica (WinForms)
 └──  Tests/           # Pruebas unitarias (si aplica)

## Instrucciones de Ejecución
Clonar el repositorio: Descarga el código fuente a tu máquina local.

>>Configurar Base de Datos:

Ejecuta el script SQL proporcionado (script_bd.sql si existe) para crear las tablas y poblar los datos maestros (Estados, Inspectores).
Abre el archivo de configuración (o la clase de conexión en DAO) y ajusta la Connection String a tu instancia de SQL Server local.
Compilar: Abre la solución .sln en Visual Studio y compila el proyecto para restaurar dependencias.
Ejecutar: Inicia la aplicación. Deberías ver el formulario principal para comenzar a gestionar órdenes.

##DDL sql server
-- =====================================================================
-- DDL SEGURO PARA SISTEMA DE INSPECCIÓN SISMOLÓGICA
-- =====================================================================
-- ADVERTENCIA: Este script borra y recrea tablas. Hacé backup si hay datos.
-- ---------------------------------------------------------------------

-- 1) Eliminar constraints si existen (uso de sys.foreign_keys)
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_EMPLEADO_ROL')
    ALTER TABLE dbo.EMPLEADO DROP CONSTRAINT FK_EMPLEADO_ROL;
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_USUARIO_EMPLEADO')
    ALTER TABLE dbo.USUARIO DROP CONSTRAINT FK_USUARIO_EMPLEADO;
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_ORDEN_ESTADO')
    ALTER TABLE dbo.ORDEN_DE_INSPECCION DROP CONSTRAINT FK_ORDEN_ESTADO;
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_ORDEN_EMPLEADO')
    ALTER TABLE dbo.ORDEN_DE_INSPECCION DROP CONSTRAINT FK_ORDEN_EMPLEADO;
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_ORDEN_ESTACION_SISMOLOGICA')
    ALTER TABLE dbo.ORDEN_DE_INSPECCION DROP CONSTRAINT FK_ORDEN_ESTACION_SISMOLOGICA;
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_SISMOGRAFO_ESTADO')
    ALTER TABLE dbo.SISMOGRAFO DROP CONSTRAINT FK_SISMOGRAFO_ESTADO;
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_ESTACION_SISMOGRAFO')
    ALTER TABLE dbo.ESTACION_SISMOLOGICA DROP CONSTRAINT FK_ESTACION_SISMOGRAFO;
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_CAMBIO_ESTADO_EMPLEADO')
    ALTER TABLE dbo.CAMBIO_ESTADO DROP CONSTRAINT FK_CAMBIO_ESTADO_EMPLEADO;
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_CAMBIO_ESTADO_SISMOGRAFO')
    ALTER TABLE dbo.CAMBIO_ESTADO DROP CONSTRAINT FK_CAMBIO_ESTADO_SISMOGRAFO;
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_MOTIVO_TIPO')
    ALTER TABLE dbo.MOTIVO_FUERA_SERVICIO DROP CONSTRAINT FK_MOTIVO_TIPO;
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_MOTIVO_CAMBIO_ESTADO')
    ALTER TABLE dbo.MOTIVO_FUERA_SERVICIO DROP CONSTRAINT FK_MOTIVO_CAMBIO_ESTADO;

-- 2) Borrar tablas y secuencias (IF EXISTS, SQL Server 2016+)
DROP TABLE IF EXISTS dbo.MOTIVO_FUERA_SERVICIO;
DROP TABLE IF EXISTS dbo.CAMBIO_ESTADO;
DROP TABLE IF EXISTS dbo.ORDEN_DE_INSPECCION;
DROP TABLE IF EXISTS dbo.ESTACION_SISMOLOGICA;
DROP TABLE IF EXISTS dbo.SISMOGRAFO;
DROP TABLE IF EXISTS dbo.EMPLEADO;
DROP TABLE IF EXISTS dbo.USUARIO;
DROP TABLE IF EXISTS dbo.ROL;
DROP TABLE IF EXISTS dbo.ESTADO;
DROP TABLE IF EXISTS dbo.MOTIVO_TIPO;

DROP SEQUENCE IF EXISTS SEQ_MOTIVO_TIPO_ID;
DROP SEQUENCE IF EXISTS SEQ_MOTIVO_ID;
DROP SEQUENCE IF EXISTS SEQ_CAMBIO_ESTADO_ID;
DROP SEQUENCE IF EXISTS SEQ_ORDEN_ID;
DROP SEQUENCE IF EXISTS SEQ_ESTACION_ID;
DROP SEQUENCE IF EXISTS SEQ_SISMOGRAFO_ID;
DROP SEQUENCE IF EXISTS SEQ_EMPLEADO_ID;
DROP SEQUENCE IF EXISTS SEQ_ROL_ID;
DROP SEQUENCE IF EXISTS SEQ_ESTADO_ID;

-- 3) Secuencias
CREATE SEQUENCE SEQ_ROL_ID START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE SEQ_EMPLEADO_ID START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE SEQ_ESTADO_ID START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE SEQ_ORDEN_ID START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE SEQ_SISMOGRAFO_ID START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE SEQ_ESTACION_ID START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE SEQ_CAMBIO_ESTADO_ID START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE SEQ_MOTIVO_ID START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE SEQ_MOTIVO_TIPO_ID START WITH 1 INCREMENT BY 1;

-- 4) Tablas

CREATE TABLE dbo.ROL (
    ID_ROL INT NOT NULL DEFAULT NEXT VALUE FOR SEQ_ROL_ID,
    NOMBRE NVARCHAR(100) NOT NULL,
    DESCRIPCION_ROL NVARCHAR(255) NULL,
    CONSTRAINT PK_ROL PRIMARY KEY (ID_ROL)
);

CREATE TABLE dbo.ESTADO (
    ID_ESTADO INT NOT NULL DEFAULT NEXT VALUE FOR SEQ_ESTADO_ID,
    AMBITO NVARCHAR(50) NULL,
    NOMBRE_ESTADO NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_ESTADO PRIMARY KEY (ID_ESTADO)
);

CREATE TABLE dbo.MOTIVO_TIPO (
    ID_MOTIVO_TIPO INT NOT NULL DEFAULT NEXT VALUE FOR SEQ_MOTIVO_TIPO_ID,
    DESCRIPCION NVARCHAR(255) NOT NULL,
    CONSTRAINT PK_MOTIVO_TIPO PRIMARY KEY (ID_MOTIVO_TIPO)
);

CREATE TABLE dbo.EMPLEADO (
    ID_EMPLEADO INT NOT NULL DEFAULT NEXT VALUE FOR SEQ_EMPLEADO_ID,
    NOMBRE NVARCHAR(100) NOT NULL,
    APELLIDO NVARCHAR(100) NOT NULL,
    TELEFONO NVARCHAR(50) NULL,
    EMAIL NVARCHAR(100) NULL,
    ID_ROL INT NOT NULL,
    CONSTRAINT PK_EMPLEADO PRIMARY KEY (ID_EMPLEADO)
);
ALTER TABLE dbo.EMPLEADO
    ADD CONSTRAINT FK_EMPLEADO_ROL FOREIGN KEY (ID_ROL) REFERENCES dbo.ROL(ID_ROL) ON DELETE NO ACTION ON UPDATE NO ACTION;

CREATE TABLE dbo.SISMOGRAFO (
    ID_SISMOGRAFO INT NOT NULL DEFAULT NEXT VALUE FOR SEQ_SISMOGRAFO_ID,
    FECHA_ADQUISICION DATETIME NULL,
    IDENTIFICADOR_SISMOGRAFO NVARCHAR(100) NULL,
    NUMERO_SERIE NVARCHAR(100) NULL,
    ID_ESTADO INT NOT NULL,
    CONSTRAINT PK_SISMOGRAFO PRIMARY KEY (ID_SISMOGRAFO)
);
ALTER TABLE dbo.SISMOGRAFO
    ADD CONSTRAINT FK_SISMOGRAFO_ESTADO FOREIGN KEY (ID_ESTADO) REFERENCES dbo.ESTADO(ID_ESTADO) ON DELETE NO ACTION ON UPDATE NO ACTION;

CREATE TABLE dbo.CAMBIO_ESTADO (
    ID_CAMBIO_ESTADO INT NOT NULL DEFAULT NEXT VALUE FOR SEQ_CAMBIO_ESTADO_ID,
    FECHA_HORA_INICIO DATETIME NOT NULL CONSTRAINT DF_CAMBIO_FECHA_INICIO DEFAULT (GETDATE()),
    FECHA_HORA_FIN DATETIME NULL,
    ID_EMPLEADO INT NOT NULL,
    ID_SISMOGRAFO INT NULL,
    CONSTRAINT PK_CAMBIO_ESTADO PRIMARY KEY (ID_CAMBIO_ESTADO)
);
ALTER TABLE dbo.CAMBIO_ESTADO
    ADD CONSTRAINT FK_CAMBIO_ESTADO_EMPLEADO FOREIGN KEY (ID_EMPLEADO) REFERENCES dbo.EMPLEADO(ID_EMPLEADO) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE dbo.CAMBIO_ESTADO
    ADD CONSTRAINT FK_CAMBIO_ESTADO_SISMOGRAFO FOREIGN KEY (ID_SISMOGRAFO) REFERENCES dbo.SISMOGRAFO(ID_SISMOGRAFO) ON DELETE NO ACTION ON UPDATE NO ACTION;

CREATE TABLE dbo.MOTIVO_FUERA_SERVICIO (
    ID_MOTIVO INT NOT NULL DEFAULT NEXT VALUE FOR SEQ_MOTIVO_ID,
    COMENTARIO NVARCHAR(255) NULL,
    ID_MOTIVO_TIPO INT NOT NULL,
    ID_CAMBIO_ESTADO INT NOT NULL,
    CONSTRAINT PK_MOTIVO PRIMARY KEY (ID_MOTIVO)
);
ALTER TABLE dbo.MOTIVO_FUERA_SERVICIO
    ADD CONSTRAINT FK_MOTIVO_TIPO FOREIGN KEY (ID_MOTIVO_TIPO) REFERENCES dbo.MOTIVO_TIPO(ID_MOTIVO_TIPO) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE dbo.MOTIVO_FUERA_SERVICIO
    ADD CONSTRAINT FK_MOTIVO_CAMBIO_ESTADO FOREIGN KEY (ID_CAMBIO_ESTADO) REFERENCES dbo.CAMBIO_ESTADO(ID_CAMBIO_ESTADO) ON DELETE NO ACTION ON UPDATE NO ACTION;

CREATE TABLE dbo.USUARIO (
    NOMBRE_USUARIO NVARCHAR(50) NOT NULL,
    CONTRASENA NVARCHAR(200) NOT NULL,
    ID_EMPLEADO INT NOT NULL,
    CONSTRAINT PK_USUARIO PRIMARY KEY (NOMBRE_USUARIO)
);
ALTER TABLE dbo.USUARIO
    ADD CONSTRAINT FK_USUARIO_EMPLEADO FOREIGN KEY (ID_EMPLEADO) REFERENCES dbo.EMPLEADO(ID_EMPLEADO) ON DELETE NO ACTION ON UPDATE NO ACTION;

CREATE TABLE dbo.ESTACION_SISMOLOGICA (
    ID_ESTACION_SISMOLOGICA INT NOT NULL DEFAULT NEXT VALUE FOR SEQ_ESTACION_ID,
    CODIGO_ESTACION NVARCHAR(50) NULL,
    DOCUMENTO_CERTIFICACION_ADQ NVARCHAR(255) NULL,
    FECHA_CERTIFICACION DATETIME NULL,
    LATITUD DECIMAL(9,6) NULL,
    LONGITUD DECIMAL(9,6) NULL,
    NOMBRE NVARCHAR(100) NULL,
    NRO_CERTIFICACION_ADQ NVARCHAR(100) NULL,
    ID_SISMOGRAFO INT NOT NULL,
    CONSTRAINT PK_ESTACION PRIMARY KEY (ID_ESTACION_SISMOLOGICA)
);
ALTER TABLE dbo.ESTACION_SISMOLOGICA
    ADD CONSTRAINT FK_ESTACION_SISMOGRAFO FOREIGN KEY (ID_SISMOGRAFO) REFERENCES dbo.SISMOGRAFO(ID_SISMOGRAFO) ON DELETE NO ACTION ON UPDATE NO ACTION;

CREATE TABLE dbo.ORDEN_DE_INSPECCION (
    ID_ORDEN INT NOT NULL DEFAULT NEXT VALUE FOR SEQ_ORDEN_ID,
    FECHA_HORA_CIERRE DATETIME NULL,
    FECHA_HORA_FIN DATETIME NULL,
    FECHA_HORA_INICIO DATETIME NULL,
    NRO_ORDEN INT NOT NULL,
    OBSERVACION_CIERRE NVARCHAR(255) NULL,
    ID_ESTADO INT NOT NULL,
    ID_EMPLEADO INT NOT NULL,
    ID_ESTACION INT NOT NULL,
    CONSTRAINT PK_ORDEN PRIMARY KEY (ID_ORDEN)
);
ALTER TABLE dbo.ORDEN_DE_INSPECCION
    ADD CONSTRAINT FK_ORDEN_ESTADO FOREIGN KEY (ID_ESTADO) REFERENCES dbo.ESTADO(ID_ESTADO) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE dbo.ORDEN_DE_INSPECCION
    ADD CONSTRAINT FK_ORDEN_EMPLEADO FOREIGN KEY (ID_EMPLEADO) REFERENCES dbo.EMPLEADO(ID_EMPLEADO) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE dbo.ORDEN_DE_INSPECCION
    ADD CONSTRAINT FK_ORDEN_ESTACION_SISMOLOGICA FOREIGN KEY (ID_ESTACION) REFERENCES dbo.ESTACION_SISMOLOGICA(ID_ESTACION_SISMOLOGICA) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- 5) Índices útiles
CREATE UNIQUE INDEX UX_ORDEN_NROORDEN ON dbo.ORDEN_DE_INSPECCION(NRO_ORDEN);
CREATE INDEX IX_ORDEN_ID_ESTADO ON dbo.ORDEN_DE_INSPECCION(ID_ESTADO);
CREATE INDEX IX_ORDEN_ID_EMPLEADO ON dbo.ORDEN_DE_INSPECCION(ID_EMPLEADO);
CREATE INDEX IX_ORDEN_ID_ESTACION ON dbo.ORDEN_DE_INSPECCION(ID_ESTACION);
CREATE INDEX IX_CAMBIO_ID_SISMOGRAFO ON dbo.CAMBIO_ESTADO(ID_SISMOGRAFO);
CREATE INDEX IX_SISMOGRAFO_ID_ESTADO ON dbo.SISMOGRAFO(ID_ESTADO);

-- =====================================================================
-- FIN DDL
-- =====================================================================
