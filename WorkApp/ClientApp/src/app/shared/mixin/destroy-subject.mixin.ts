import { Subject } from 'rxjs';

type Constructor<T = {}> = new (...args: any[]) => T;

export const destroySubjectMixin = <T extends Constructor>(base: T = class { } as T) =>
  class extends base {
    protected destroy$ = new Subject<boolean>();
    constructor(...args) {
      super(...args);
      const onDestroy = (this as any).ngOnDestroy;

      if (!onDestroy) {
        throw new Error(`Class must implement OnDestroy`);
      }
      (this as any).ngOnDestroy = () => {
        if (onDestroy) {
          onDestroy.apply(this);
        }
        this.destroy$.next();
        this.destroy$.complete();
      };
    }
}