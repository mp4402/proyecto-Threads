#include <pthread.h>
#include <stdio.h>
#include <string.h>
#include <stdbool.h>
#include <stdlib.h>
#include <unistd.h>
#include <ctype.h>

#define MAX_THREADS 500
#define MAXCHAR 1000

pthread_t thread_id[MAX_THREADS];    

void * Open_file(void * data)
{
FILE *fptr;
char row[MAXCHAR];
char *token,*ret;
char c;
double entero,columna,c1,c2;
printf("Hello from thread %u - I was created in iteration %d !\n", (int)pthread_self(), (int)data);
    fptr = fopen("t.csv","r");
    entero=0;
    columna=0;
    c1=0;
    c2=0;
    while (!feof(fptr)) {
	fgets(row,MAXCHAR,fptr);
	token = strtok(row,",");
	while(token!=NULL) {
		ret=strstr(token,"-");
		if (ret == NULL){
			if (columna==0)
				c1+=atof(token);
			else if (columna==1)
				c2+=atof(token);
			columna++;
		}
		else{
			columna=0;
		}
		token = strtok(NULL,",");
	}
    }
		printf("Thread %u - created in ite: %d row: %s token: %s %f %f!\n", (int)pthread_self(), (int)data,row,token,c1,c2);
    fclose(fptr);
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

    for(i = 0; i < n; i++)
    {
        rc = pthread_create(&thread_id[i], NULL, Open_file, (void*)i);
        if(rc)
        {
             printf("\n ERROR: return code from pthread_create is %d \n", rc);
             exit(1);
        }
        printf("\n Thread %u. Created new thread (%u) in ite %d ...\n",(int)pthread_self(), (int)thread_id[i], i);
        //if(i % 5 == 0) sleep(1);
    }
    pthread_exit(NULL);
}