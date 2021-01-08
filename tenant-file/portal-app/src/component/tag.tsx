import React from 'react';
import styled from 'styled-components';
import { isDark } from '../utility';

const StyledTag = styled.button`
  display: inline-block;
  padding: 0.1rem 0.5rem;
  background-color: ${(props) => props.color};
  border-radius: 15px;
  font-weight: 700;
  color: ${(props) => {
    return isDark(props.color!) ? 'white' : 'black';
  }};
  box-shadow: none;
  border: none;
`;

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
