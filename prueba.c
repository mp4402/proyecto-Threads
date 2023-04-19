#include <pthread.h>
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>

#include <string.h>
#include <math.h>
#include <ctype.h>

#define MAX_THREADS 50

pthread_t thread_id[MAX_THREADS];    

void * PrintHello(void * data)
{
    //printf("Hello from thread %u - I was created in iteration %d !\n", (int)pthread_self(), (int)data);
    char file[300];
    int i;
    char line[200];
    char *sp;
    FILE *fp;
    // Read in your file here ..
    // variables que van a ir reciviendo el valor.
    double open, high, low, close; 
    //char fecha[15]; 
    // Variables min max
    double min_open = 0, min_high = 0, min_low = 0, min_close = 0;
    double max_open = 0, max_high = 0, max_low = 0, max_close = 0;
    // Variables que van a ir acumulando el valor
    double topen = 0, thigh = 0, tlow = 0, tclose = 0;
    double total = 0;
    // medias
    float media_open, media_high, media_low, media_close;
    // Variables para leer la linea y sus valores. 
    int mov;
    for (i = ((int)data - 249); i <= (int)data-1; i++) {
        snprintf(file, 300, "/Users/carlosalvarado/Desktop/SO/proyecto-Threads/Docker_proyecto/so_data/index_data_%d.csv", i);
        fp = fopen(file, "r");
        if (fp == NULL){
            printf("Trouble reading file ! Program Tereminating ...\n ");
            printf("El file es: %s \n", file);
            //fclose(fp);
            //exit(0);
        }else{
            //printf("Si abro el archivo\n");
            //printf("El file es: %s \n", file);
            mov = 2;
             while (!feof(fp)){
                //printf("\nEmpieza linea! \n");
                fgets(line, 200, fp);
                sp = strtok(line, ","); // leo la fecha
                total = total + 1; 
                
                sp = strtok(NULL, ",");// leo open
                while(sp!=NULL) {
                    switch (mov)
                    {
                        case 2: // open
                            if(isdigit(*sp)>0){
                                //printf("El thread: %u -- El mov es: %d, luego el sp(token): %s \n", (int)pthread_self(),mov, sp);
                                open = atof(sp);
                                topen += open;
                                if (min_open > open || total == 2){
                                    min_open = open;
                                }
                                if(max_open < open || total == 1){
                                    max_open = open;
                                }
                                mov += 1;
                            }
                            break;
                        case 3: // high
                            if(isdigit(*sp)>0){
                                //printf("El thread: %u -- El mov es: %d, luego el sp(token): %s \n", (int)pthread_self(),mov, sp);
                                high = atof(sp);
                                thigh += high;
                                if (min_high > high || total == 2){
                                    min_high = high;
                                }
                                if(max_high < high || total == 1){
                                    max_high = high;
                                }
                                mov += 1;
                            }
                            break;
                        case 4: // low
                            if(isdigit(*sp)>0){
                                //printf("El thread: %u -- El mov es: %d, luego el sp(token): %s \n", (int)pthread_self(),mov, sp);
                                low = atof(sp);
                                tlow += low;
                                if (min_low > low || total == 2){
                                    min_low = low;
                                }
                                if(max_low < low || total == 1){
                                    max_low = low;
                                }
                                mov += 1;
                            }
                            break;
                        case 5: // close
                            if(isdigit(*sp)>0){
                                //printf("El thread: %u -- El mov es: %d, luego el sp(token): %s \n", (int)pthread_self(),mov, sp);
                                close = atof(sp);
                                tclose += close;
                                if (min_close > close || total == 2){
                                    min_close = close;
                                }
                                if(max_close < close || total == 1){
                                    max_close = close;
                                }
                                mov += 1;
                            }
                            break;
                        
                        default:
                            printf("El thread: %u -- El mov es: %d, luego el sp(token): %s \n", (int)pthread_self(),mov, sp);
                            //mov -= 1;
                            break;
                    }
                    sp = strtok(NULL, ",");
                } 
                mov = 2;
            }
            fclose(fp);


            
            media_open = topen/(total-1);
            media_high = thigh/(total-1);
            media_low = tlow/(total-1);
            media_close = tclose/(total-1);
            snprintf(file, 300, "/Users/carlosalvarado/Desktop/SO/proyecto-Threads/Docker_proyecto/so_respuesta/archivo_out_%d.csv", i);
            fp = fopen(file, "w+");
            //printf("Aquí estoy escribiendo en el file: %s", file);
            fprintf(fp,"Open,High, Low, Close\n");
            fprintf(fp,"Count, %.2f, %.2f, %.2f, %.2f, \n", total, total, total, total);
            fprintf(fp,"Mean, %.2f, %.2f, %.2f, %.2f, \n", media_open, media_high, media_low, media_close);
            //fprintf(fp,"Std, %.2f, %.2f, %.2f, %.2f, \n", sqrt(acum_open/(total-1)), sqrt(acum_high/(total-1)), sqrt(acum_low/(total-1)), sqrt(acum_close/(total-1)));
            fprintf(fp,"Min, %.2f, %.2f, %.2f, %.2f, \n", min_open, min_high, min_low, min_close);
            fprintf(fp,"Max,%.2f, %.2f, %.2f, %.2f, \n", max_open, max_high, max_low, max_close);
            fclose(fp);
        }
    }
    pthread_exit(NULL);
}

void * un_hilo(void * data)
{
    //printf("Hello from thread %u - I was created in iteration %d !\n", (int)pthread_self(), (int)data);
    char file[300];
    int i;
    char line[200];
    char *sp;
    FILE *fp;
    // Read in your file here ..
    // variables que van a ir reciviendo el valor.
    double open, high, low, close; 
    //char fecha[15]; 
    // Variables min max
    double min_open = 0, min_high = 0, min_low = 0, min_close = 0;
    double max_open = 0, max_high = 0, max_low = 0, max_close = 0;
    // Variables que van a ir acumulando el valor
    double topen = 0, thigh = 0, tlow = 0, tclose = 0;
    double total = 0;
    // Variables para leer la linea y sus valores. 
    int mov;
    for (i = ((int)data - 250); i < (int)data; i++) {
    //for (i = 1; i < (int)data+2; i++) {
        snprintf(file, 300, "/Users/carlosalvarado/Desktop/SO/proyecto-Threads/Docker_proyecto/so_data/index_data_%d.csv", i);
        fp = fopen(file, "r");
        if (fp == NULL){
            printf("Trouble reading file ! Program Tereminating ...\n ");
            //fclose(fp);
            //exit(0);
        }else{
            printf("Si abro el archivo\n");
            printf("El file es: %s \n", file);
            mov = 2;
             while (!feof(fp)){
                printf("\nEmpieza linea! \n");
                fgets(line, 200, fp);
                sp = strtok(line, ","); // leo la fecha
                total = total + 1; 
                
                sp = strtok(NULL, ",");
                while(sp!=NULL) {
                    // leo open
                    printf("El thread: %u -- El mov es: %d, luego el sp(token): %s \n", (int)pthread_self(),mov, sp);
                    switch (mov)
                    {
                    case 2: // open
                        open = atof(sp);
                        topen += open;
                        if (min_open > open || total == 2){
                            min_open = open;
                        }
                        if(max_open < open || total == 1){
                            max_open = open;
                        }
                        break;
                    case 3: // high
                        high = atof(sp);
                        thigh += high;
                        if (min_high > high || total == 2){
                            min_high = high;
                        }
                        if(max_high < high || total == 1){
                            max_high = high;
                        }
                        break;
                    case 4: // low
                        low = atof(sp);
                        tlow += low;
                        if (min_low > low || total == 2){
                            min_low = low;
                        }
                        if(max_low < low || total == 1){
                            max_low = low;
                        }
                        break;
                    case 5: // close
                        close = atof(sp);
                        tclose += close;
                        if (min_close > close || total == 2){
                            min_close = close;
                        }
                        if(max_close < close || total == 1){
                            max_close = close;
                        }
                        break;
                    
                    default:
                        break;
                    }
                    sp = strtok(NULL, ",");
                    mov += 1;

                    //sp = NULL;
                    //printf("El mov es: %d\n",mov);
                    
                } 
                mov = 2;
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
    printf("El valor de n es: %d \n", n);
    //sleep(10);
    switch (n)
    {
        case 1:
            x = 1;
            //aumento = x;
            for(i = 0; i < n; i++)
            {
                rc = pthread_create(&thread_id[i], NULL, un_hilo, (void*)x);
                //x = x + aumento;
                if(rc)
                {
                    printf("\n ERROR: return code from pthread_create is %d \n", rc);
                    exit(1);
                }
                printf("\n I am thread %u. Created new thread (%u) in iteration %d ...\n", 
                        (int)pthread_self(), (int)thread_id[i], i);
            }
            break;
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
                //printf("\n I am thread %u. Created new thread (%u) in iteration %d ...\n", 
                        //(int)pthread_self(), (int)thread_id[i], i);
            }
            
            break;
        
    default:
        break;
    }

    pthread_exit(NULL);
}