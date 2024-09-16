import { Component, OnInit } from '@angular/core';
import { PostService } from '../services/post.service';
import { Subject, takeUntil } from 'rxjs';
import { PageResultDto } from '../../../../shared-lib/src/lib/models/page-result.dto';
import { PostDto } from '../entites/post.dto';
import { format } from 'date-fns';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrl: './post.component.scss',
})
export class PostComponent implements OnInit {
  private ngUnsubscribe = new Subject<void>();
  postDtos: PostDto[] = [];
  postTime: number = 0;

  constructor(
    private postService: PostService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getAllPost();
  }

  getAllPost() {
    this.postService
      .getPosts(10, 1)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PageResultDto<PostDto>) => {
          this.postDtos = response.items;
        },
        error: () => {},
      });
  }

  formatDate(date: Date): string {
    return format(date, 'yyyy-MM-dd HH:mm:ss');
  }

  redirectToDetailPost(id: string) {
    this.postService.setIdPost(id);
    this.router.navigate(['detail'], {
      relativeTo: this.route,
    });
  }
}
