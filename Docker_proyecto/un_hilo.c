#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
// prueba
#include <unistd.h>
#include <time.h>
#include <sys/wait.h>



void lectura (FILE *fp, int num){
    char file[35];
    snprintf(file, 35, "so_data/index_data_%d.csv", num);
    fp = fopen(file, "r");
    if (fp == NULL){
        printf("Trouble reading file ! \nProgram Tereminating ... ");
        exit(0);
    }
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
    while ( fgets(line, 200, fp) != NULL){
        //printf("\n%s", line);
        sp = strtok(line, ","); // leo la fecha
        total = total + 1; 
        sp = strtok(NULL, ","); // leo open
        open = atof(sp);
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
        }

    }
    fclose(fp);
    float media_open, media_high, media_low, media_close;
    media_open = topen/(total-1);
    media_high = thigh/(total-1);
    media_low = tlow/(total-1);
    media_close = tclose/(total-1);
    fp = fopen(file, "r");
    if (fp == NULL){
        printf("Trouble reading file ! \nProgram Tereminating ... ");
        exit(0);
    }
    float acum_open = 0, acum_high = 0, acum_low = 0, acum_close = 0;
    total = 0;
    while (fgets(line, 200, fp) != NULL){
        sp = strtok(line, ","); // leo la fecha
        total = total + 1; 
        sp = strtok(NULL, ","); // leo open
        open = atof(sp);
        if (total >= 2.0){
            acum_open += pow(open - media_open, 2);
        }
        sp = strtok(NULL, ","); // leo high
        high = atof(sp);
        if (total >= 2.0){
            acum_high += pow(high - media_high, 2);
        }
        sp = strtok(NULL, ","); // leo low
        low = atof(sp);
        if (total >= 2.0){
            acum_low += pow(low - media_low, 2);
        }
        sp = strtok(NULL, ","); // leo close
        close = atof(sp);
        if (total >= 2.0){
            acum_close += pow(close - media_close, 2);
        }
    }
    fclose(fp);
    snprintf(file, 35, "so_respuesta/archivo_out_%d.csv", num);
    fp = fopen(file, "w+");
    fprintf(fp,"Open,High, Low, Close\n");
    fprintf(fp,"Count, %.2f, %.2f, %.2f, %.2f, \n", total, total, total, total);
    fprintf(fp,"Mean, %.2f, %.2f, %.2f, %.2f, \n", media_open, media_high, media_low, media_close);
    fprintf(fp,"Std, %.2f, %.2f, %.2f, %.2f, \n", sqrt(acum_open/(total-1)), sqrt(acum_high/(total-1)), sqrt(acum_low/(total-1)), sqrt(acum_close/(total-1)));
    fprintf(fp,"Min, %.2f, %.2f, %.2f, %.2f, \n", min_open, min_high, min_low, min_close);
    fprintf(fp,"Max,%.2f, %.2f, %.2f, %.2f, \n", max_open, max_high, max_low, max_close);
    fclose(fp);
}

int main() {
    FILE *fp;
    int i;
    for (i = 1; i <= 999; i++) {
      lectura(fp, i);
    }
    return 0;
}
