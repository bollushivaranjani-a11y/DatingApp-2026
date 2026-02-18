import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError } from 'rxjs';
import { ToastService } from '../services/toast-service';
import { NavigationExtras, Router } from '@angular/router'; 

export const errorInterceptor: HttpInterceptorFn = (req, next) => {

const toast = inject(ToastService);
const router = inject(Router);

  return next(req).pipe( 
    catchError(msg => {
      if(msg)
      {
        switch(msg.status)
        {
          case 400:
              if(msg.error.errors )
              {
                const modelStateErrors = [];
                for(const key in msg.error.errors)
                {
                  if(msg.error.errors[key])
                  {
                    modelStateErrors.push(msg.error.errors[key]);
                  }
                  
                }
                throw modelStateErrors.flat();
               }
               else
                {
                  toast.error(msg.error,5000);
                }
              break;

                 case 401:
              toast.error('Unauthorized',5000);
              break;

               case 404:
             // toast.error('NotFound',5000);
              router.navigateByUrl('/not-found');
              break;

              case 500:
                const navigationExtra : NavigationExtras = {state : {error : msg.error}}
                router.navigateByUrl('/server-error', navigationExtra);
             // toast.error('Server Error',5000);
              break;

          default:
             toast.error('Something went wrong',5000);
            break;
        }
      }
      throw msg;
    })
  )
};