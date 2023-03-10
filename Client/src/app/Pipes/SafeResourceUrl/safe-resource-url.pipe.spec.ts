import { TestBed } from '@angular/core/testing';
import { SafeResourceUrlPipe } from './safe-resource-url.pipe';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

describe('SafeResourceUrlPipe', () => {
  let pipe: SafeResourceUrlPipe;
  let sanitizer: DomSanitizer;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DomSanitizer],
      declarations: [SafeResourceUrlPipe]
    });

    pipe = TestBed.inject(SafeResourceUrlPipe);
    sanitizer = TestBed.inject(DomSanitizer);
  });

  it('create an instance', () => {
    expect(pipe).toBeTruthy();
  });

  it('transforms unsafe URL into a safe URL', () => {
    const unsafeUrl = 'javascript:alert("Hello World")';
    const safeUrl: SafeResourceUrl = pipe.transform(unsafeUrl);
    expect(safeUrl).toEqual(sanitizer.bypassSecurityTrustResourceUrl(unsafeUrl));
  });
});
