#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int main() {
    FILE *fp;

    fp = fopen("index_data_1.csv", "r");

    if (fp == NULL){
        printf("Trouble reading file ! \nProgram Tereminating ... ");
        exit(0);
    }
    // Read in your file here ..
    // variables que van a ir reciviendo el valor.
    float open, high, low, close; 
    //char fecha[15]; 
    // Variables min max
    float min_open = 10000, min_high = 10000, min_low = 10000, min_close = 10000;
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
        total = total + 1.0; 
        sp = strtok(NULL, ","); // leo open
        open = atof(sp);
        topen = topen + open;
        if (min_open > open){
            min_open = open;
        } else {
            if (total == 1.0){
                printf("Primer ciclo");
                min_open = open;
            }
        }
        if(max_open < open | total == 1){
            max_open = open;
        }
        sp = strtok(NULL, ","); // leo high
        high = atof(sp);
        thigh = thigh + high;
        if (min_high > high){
            min_high = high;
        }
        if(max_high < high | total == 1){
            max_high = high;
        }
        sp = strtok(NULL, ","); // leo low
        low = atof(sp);
        tlow = tlow + low;
        if (min_low > low){
            min_low = low;
        }
        if(max_low < low || total == 1){
            max_low = low;
        }
        sp = strtok(NULL, ","); // leo close
        close = atof(sp);
        tclose = tclose + close;
        if (min_close > close){
            min_close = close;
        }
        if(max_close < close || total == 1){
            max_close = close;
        }

    }

    printf("      | Open   | High   | Low   | Close   | \n");
    printf("Count | %.2f | %.2f | %.2f | %.2f | \n", total, total, total, total);
    printf("Mean  | %.2f | %.2f | %.2f | %.2f | \n", topen/(total-1), thigh/(total-1), tlow/(total-1), tclose/(total-1));
    printf("Min   | %.2f | %.2f | %.2f | %.2f | \n", min_open, min_high, min_low, min_close);
    printf("Max   | %.2f | %.2f | %.2f | %.2f | \n", max_open, max_high, max_low, max_close);

    fclose(fp);
    return 0;

} 