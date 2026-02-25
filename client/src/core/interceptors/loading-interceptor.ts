import { HttpEvent, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { BusyService } from '../services/busy-service';
import { delay, finalize, of, tap } from 'rxjs';

const cache = new Map<string, HttpEvent<unknown>>();

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  
  const busyService = inject(BusyService);
if(req.method == 'GET')
{
  const cacheresponse  = cache.get(req.url);

  if(cacheresponse)
  {
    return of(cacheresponse);
  }
}
  busyService.busy();
  
  return next(req).pipe(
    delay(500), 
    tap((response: HttpEvent<unknown>) => {
      cache.set(req.url,response)
    }),
    finalize(() => { busyService.Idle()
    })
  )
};

/// caching urls to avoid loading and to improve performance