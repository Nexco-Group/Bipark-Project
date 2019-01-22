ESQuery is a library for querying the AST output by Esprima for patterns of syntax using a CSS style selector system. Check out the demo:

[demo](https://estools.github.io/esquery/)

The following selectors are supported:
* AST node type: `ForStatement`
* [wildcard](https://dev.w3.org/csswg/selectors4/#universal-selector): `*`
* [attribute existence](https://dev.w3.org/csswg/selectors4/#attribute-selectors): `[attr]`
* [attribute value](https://dev.w3.org/csswg/selectors4/#attribute-selectors): `[attr="foo"]` or `[attr=123]`
* attribute regex: `[attr=/foo.*/]`
* attribute conditons: `[attr!="foo"]`, `[attr>2]`, `[attr<3]`, `[attr>=2]`, or `[attr<=3]` 
* nested attribute: `[attr.level2="foo"]`
* field: `FunctionDeclaration > Identifier.id`
* [First](https://dev.w3.org/csswg/selectors4/#the-first-child-pseudo) or [last](https://dev.w3.org/csswg/selectors4/#the-last-child-pseudo) child: `:first-child` or `:last-child`
* [nth-child](https://dev.w3.org/csswg/selectors4/#the-nth-child-pseudo) (no ax+b support): `:nth-child(2)`
* [nth-last-child](https://dev.w3.org/csswg/selectors4/#the-nth-last-child-pseudo) (no ax+b support): `:nth-last-child(1)`
* [descendant](https://dev.w3.org/csswg/selectors4/#descendant-combinators): `ancestor descendant`
* [child](https://dev.w3.org/csswg/selectors4/#child-combinators): `parent > child`
* [following sibling](https://dev.w3.org/csswg/selectors4/#general-sibling-combinators): `node ~ sibling`
* [adjacent sibling](https://dev.w3.org/csswg/selectors4/#adjacent-sibling-combinators): `node + adjacent`
* [negation](https://dev.w3.org/csswg/selectors4/#negation-pseudo): `:not(ForStatement)`
* [matches-any](https://dev.w3.org/csswg/selectors4/#matches): `:matches([attr] > :first-child, :last-child)`
* [subject indicator](https://dev.w3.org/csswg/selectors4/#subject): `!IfStatement > [name="foo"]`
* class of AST node: `:statement`, `:expression`, `:declaration`, `:function`, or `:pattern`

[![Build Status](https://travis-ci.org/estools/esquery.png?branch=master)](https://travis-ci.org/estools/esquery)
