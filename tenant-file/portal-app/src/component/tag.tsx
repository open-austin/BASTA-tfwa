import React from 'react';
import StyledTag from './styles/Tag';
import { Tag } from '../types/tag';

type Props = {
  tag: Tag;
};

const tag = ({ tag }: Props) => {
  return <StyledTag color={tag.color}>{tag.name}</StyledTag>;
};

export default tag;
