#include <stdio.h>
#include <string.h>
int main()
{
  char a[255];
  int length;

  printf("Enter a string to calculate its length: ");
  gets(a);

  length = strlen(a);

  printf("Length of the string = %d\n", length);

  return 0;

  //docker build -t docker-world-gcc .
  //docker run -it docker-world-gcc
  
}
