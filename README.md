# Car Rental System

Un sistema de alquiler de coches implementado en .NET 8 con una API REST completa para gestionar:

- Inventario de vehículos
- Cálculo de precios dinámicos
- Devoluciones con recargos por atraso
- Seguimiento de loyalty points

## Características

### 1. Gestión de Inventario

- Tres tipos de vehículos: Premium, SUV, Small
- Control de disponibilidad
- IDs únicos para cada vehículo

### 2. Cálculo de Precios

#### Premium Cars

- **Precio por día**: 300€
- **Fórmula**: `Premium price × número de días`
- **Recargo por día extra**: Premium price + 20% = 360€/día

#### SUV Cars

- **Precio por día**: 150€
- **Primeros 7 días**: 150€/día
- **De 8 a 30 días**: 120€/día (80% de 150€)
- **Más de 30 días**: 75€/día (50% de 150€)
- **Recargo por día extra**: 150€ + (60% de 50€ small price) = 180€/día

#### Small Cars

- **Precio por día**: 50€
- **Primeros 7 días**: 50€/día
- **Más de 7 días**: 30€/día (60% de 50€)
- **Recargo por día extra**: 50€ + (30% de 50€) = 65€/día

### 3. Puntos de Lealtad

- **Premium**: 5 puntos por alquiler
- **SUV**: 3 puntos por alquiler
- **Small**: 1 punto por alquiler

## Ejemplos de Cálculo

### Alquileres

- BMW 7 (Premium) 10 días → **3000€**
- Kia Sorento (SUV) 9 días → **1290€**
- Nissan Juke (SUV) 2 días → **300€**
- Seat Ibiza (Small) 10 días → **440€**

### Recargos por Atraso

- BMW 7 (Premium) 2 días extra → **720€**
- Nissan Juke (SUV) 1 día extra → **180€**

## Instalación y Ejecución

### Requisitos

- .NET 8 SDK o superior
- Visual Studio Code, Visual Studio, o Rider

### Pasos para ejecutar

1. Navega a la carpeta del proyecto:

```bash
cd CarRentalSystem
```

2. Compila el proyecto:

```bash
dotnet build
```

3. Ejecuta la aplicación:

```bash
dotnet run
```

4. Abre tu navegador en `https://localhost:5001` (o el puerto configurado)

5. Accede a Swagger UI en `https://localhost:5001/swagger` para probar los endpoints

## Testing

Puedes probar los endpoints usando:

- **Swagger UI**: Interfaz gráfica en `/swagger`
- **Postman**: Importa los endpoints manualmente
- **REST Client** (extensión VS Code): Usa el archivo `CarRentalSystem.http`
