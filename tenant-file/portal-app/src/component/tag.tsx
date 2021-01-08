import React from 'react';
import StyledTag from './styles/Tag';

type Tag = {
  name: string;
  description: string;
  photoCount: number;
  id: number;
  color: string;
};

type Props = {
  tag: Tag;
};

const tag = ({ tag }: Props) => {
  return <StyledTag color={tag.color}>{tag.name}</StyledTag>;
};

export default tag;
