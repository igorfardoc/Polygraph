#include<iostream>
#include<ctime>
using namespace std;
int main()
{
    int n;
    cin >> n;
    while(true)
    {
        if(clock() >= n)
        {
            cout << n;
            return 0;
        }
    }
}
