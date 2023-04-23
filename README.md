# proyecto-Threads
Proyecto 2 del curso de Sistemas Operativos, referente a manejo de threads

### ¿Como ejecutar el programa?

El programa puede ser ejecutado de 2 maneras: compilado o con un contenedor de Docker

**Nota:**  debe tener los 999 archivos *index_data_#.csv* en la carpeta */your/path/.../proyecto-Threads/App/so_data*. Este repositorio no los incluye

#### Ejecucion por compilacion

**Importante:** para su ejecucion por medio de compilacion debe tener instalado .NET de la version 4.8 en adelante o un programa que lo tenga incluido (Visual Studio por ejemplo)

En caso que desee compilar desde consola debe posicionarse en la carpeta App: */your/path/.../proyecto-Threads/App*

Los pasos para la compilacion y ejecucion son los siguientes:

1. Se compila el archivo, el comando es el siguiente: *csc Program.cs*
2. Luego, se ejecuta enviando los parametros desde consola, hay varias posibilidades:
    - Tipo de paralelizacion: Puede ser paralelizacion por funciones y por archivos. Los valores posibles son:
        - Functions (para elegir paralelizacion de funciones)
        - Files (para elegir paralelizacion de archivos)
        - **Nota:** no es case sensitive, por lo que puede estar escrito en minusculas o mayusculas
    - No. de Threads a realizar: Puede ser desde 1 para una ejecución secuencial o hasta 8. Los valores posibles son:
        - 1
        - 2
        - 4
        - 8
- Por ejemplo, si se desea ejecutar una corrida con multitask de archivos con un hilo, el comando es: *Program Files 1*. Si se desea realizar paralelizacion de funciones con 4 hilos el comando es: *Program fUnCtIoNs 4*
- El programa le pedira siempre el tipo de paralelizacion y el No. de Threads a realizar, de no incluir alguno no se ejecutara

#### Ejecucion por Docker

**Importante:** debe tener Docker instalado Docker y tener Docker ejecutando al momenot de realizar los siguiente pasos:
 1. Descargar la imagen publicada en Docker Hub. El comando es el siguiente: Docker pull *mp4402/proyecto_hilos*
 2. Ejecutar la imagen de Docker en un contenedor. Esto se realiza con el comando Docker run. Hay que tener algunas consideraciones para esta parte:
    - Para que los documentos de salida sean generados en su computadora debe utilizar volumenes. La bandera en el comando es *-v /your/path/.../proyecto-Threads/App/so_respuesta:/App/so_respuesta proyecto* 
    - Para que el programa le imprima el resultado del tiempo que tardo el programa debe colocar una bandera en el comando, la cual es *-it*
    - Puede limitar el uso del cpu y de memoria RAM para dicho contenedor. Esto se realiza con las siguientes banderas: *--cpus="1.0"* *--memory="1g"*
    - **Nota:** para indicar el tipo de paralelizacion y numero de hilos se debe de colocar junto al nombre de la imagen
    - Por ejemplo, el comando completo puede ser: *docker run -it --cpus="4.0" --memory="1g" -v /your/path/.../proyecto-Threads/App/so_respuesta:/App/so_respuesta mp4402/proyecto_hilos files 4*
