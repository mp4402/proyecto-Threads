# proyecto-Threads
Proyecto 2 del curso de Sistemas Operativos, referente a manejo de threads


¿Cómo correr el programa de paralelizacion?

Al estar posicionado en la carpeta del archivo (prueba_c_hilos)

- Primero se compila el archivo, el comando es el siguiente: csc Program.cs
- Luego, se ejecuta enviando los parametros desde consola, hay varias posibilidades:
    - Tipo de paralelizacion: Puede ser paralelizacion por funciones y por archivos. Los valores posibles son:
        - Functions (para funciones)
        - Files (para archivos)
        * Nota: no es case sensitive, por lo que puede estar escrito en minusculas o mayusculas
    - No. de Threads a realizar: Puede ser desde 1 para una ejecución secuencial o hasta 8. Los valores posibles son:
        - 1
        - 2
        - 4
        - 8
- Por ejemplo, si se desea ejecutar una corrida con multitask de archivos con un hilo, el comando es: [Program Files 1]. Si se desea realizar paralelizacion de funciones con 4 hilos el comando es: [Program fUnCtIoNs 4]


