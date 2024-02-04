export interface TreeComment{
    id: number;
    author: string;
    content: string;    
    ancestorId: number | null;
    createDate: Date;
}

export interface DisplayCommentDto{
    id: number;
    author: string;
    content: string;
}

export interface CreateCommentDto{
    postId: string;
    author: string;
    content: string;
    ancestorId: number | null;
}