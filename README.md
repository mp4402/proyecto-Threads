# proyecto-Threads
Proyecto 2 del curso de Sistemas Operativos, referente a manejo de threads

### ¿Como ejecutar el programa?

El programa puede ser ejecutado de 2 maneras: compilado o con un contenedor de Docker

**Nota:** debe tener los 999 archivos index_data_#.csv en la carpeta /your/path/.../proyecto-Threads/App/so_data. Este repositorio no los incluye

#### Ejecucion por compilacion

**Importante:** para su ejecucion por medio de compilacion debe tener instalado .NET de la version 4.8 en adelante o un programa que lo tenga incluido (Visual Studio por ejemplo)

En caso que desee compilar desde consola debe posicionarse en la carpeta App: /your/path/.../proyecto-Threads/App

Los pasos para la compilacion y ejecucion son los siguientes:

1. Se compila el archivo, el comando es el siguiente: csc Program.cs
2. Luego, se ejecuta enviando los parametros desde consola, hay varias posibilidades:
    - Tipo de paralelizacion: Puede ser paralelizacion por funciones y por archivos. Los valores posibles son:
        - Functions (para funciones)
        - Files (para archivos)
        - **Nota:** no es case sensitive, por lo que puede estar escrito en minusculas o mayusculas
    - No. de Threads a realizar: Puede ser desde 1 para una ejecución secuencial o hasta 8. Los valores posibles son:
        - 1
        - 2
        - 4
        - 8
- Por ejemplo, si se desea ejecutar una corrida con multitask de archivos con un hilo, el comando es: [Program Files 1]. Si se desea realizar paralelizacion de funciones con 4 hilos el comando es: [Program fUnCtIoNs 4]
- El programa le pedirá siempre el tipo de paralelización y el No. de Threads a realizar, de no incluir alguno no se ejecutara



