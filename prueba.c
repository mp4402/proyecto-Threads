#include <pthread.h>
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>

#include <string.h>
#include <math.h>
#include <unistd.h>

#define MAX_THREADS 50

pthread_t thread_id[MAX_THREADS];    

void * PrintHello(void * data)
{
    //printf("Hello from thread %u - I was created in iteration %d !\n", (int)pthread_self(), (int)data);
    char file[300];
    int i;

    FILE *fp;
    for (i = ((int)data - 249); i <= (int)data-1; i++) {
        
        snprintf(file, 300, "/Users/carlosalvarado/Desktop/SO/proyecto-Threads/Docker_proyecto/so_data/index_data_%d.csv", i);
        fp = fopen(file, "r");
        if (fp == NULL){
            printf("Trouble reading file ! Program Tereminating ...\n ");
            //fclose(fp);
            //exit(0);
        }else{
            printf("Si abro el archivo\n");
            printf("El file es: %s \n", file);
            // Read in your file here ..
            // variables que van a ir reciviendo el valor.
            float open, high, low, close; 
            //char fecha[15]; 
            // Variables min max
            float min_open = 0, min_high = 0, min_low = 0, min_close = 0;
            float max_open = 0, max_high = 0, max_low = 0, max_close = 0;
            // Variables que van a ir acumulando el valor
            float topen = 0, thigh = 0, tlow = 0, tclose = 0;
            float total = 0;
            // Variables para leer la linea y sus valores. 
            char line[200];
            char *sp;
            /*
            fichero = fopen("libros.dat", "rt");
            while (!feof(fichero)) {
                fscanf (fichero, "%d", &valorInt);
                printf ("Extraido: %d \n", valorInt);
            }
            fclose(fichero);
    */
             while (!feof(fp)){
                char str1[10], str2[10], str3[10], str4[10];
                fgets(line, 200, fp);
                printf("\n%s", line);
                /*fscanf(fp, "%s %s %s %s", str1, str2, str3, str4);
                //break;
                total = total + 1; 
                open = atof(str2);
                topen = topen + open;
                if (min_open > open || total == 2){
                    min_open = open;
                }
                if(max_open < open || total == 1){
                    max_open = open;
                }
                
                sp = strtok(NULL, ","); // leo high
                high = atof(sp);
                thigh = thigh + high;
                if (min_high > high || total == 2){
                    min_high = high;
                }
                if(max_high < high || total == 1){
                    max_high = high;
                }
                sp = strtok(NULL, ","); // leo low
                low = atof(sp);
                tlow = tlow + low;
                if (min_low > low || total == 2){
                    min_low = low;
                }
                if(max_low < low || total == 1){
                    max_low = low;
                }
                sp = strtok(NULL, ","); // leo close
                close = atof(sp);
                tclose = tclose + close;
                if (min_close > close || total == 2){
                    min_close = close;
                }
                if(max_close < close || total == 1){
                    max_close = close;
                }*/

            }
            fclose(fp);
        }
    }
    pthread_exit(NULL);
}

int main(int argc, char * argv[])
{
    int rc, i, n;

    if(argc < 2) 
    {
        printf("Please add the number of threads to the command line\n");
        exit(1); 
    }
    n = atoi(argv[1]);
    if(n > MAX_THREADS) n = MAX_THREADS;
    int x; 
    int aumento;
    printf("El valor de n es: %d", n);
    //sleep(10);
    switch (n)
    {
        case 4:
            x = 250;
            aumento = x;
            for(i = 0; i < n; i++)
            {
                rc = pthread_create(&thread_id[i], NULL, PrintHello, (void*)x);
                x = x + aumento;
                if(rc)
                {
                    printf("\n ERROR: return code from pthread_create is %d \n", rc);
                    exit(1);
                }
                printf("\n I am thread %u. Created new thread (%u) in iteration %d ...\n", 
                        (int)pthread_self(), (int)thread_id[i], i);
                //if(i % 5 == 0) sleep(1);
            }
            
            break;
    default:
        break;
    }

    pthread_exit(NULL);
}